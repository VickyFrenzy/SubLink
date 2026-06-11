using Serilog;
using SuperSocket.ClientEngine;
using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using WebSocket4Net;
using xyz.yewnyx.SubLink.Joystick.Client.Data.Command;
using xyz.yewnyx.SubLink.Joystick.Client.Data.Event;
using xyz.yewnyx.SubLink.Joystick.Client.Data.Message;
using xyz.yewnyx.SubLink.Joystick.Client.Data.Response;
using xyz.yewnyx.SubLink.Joystick.Client.OAuth;

namespace xyz.yewnyx.SubLink.Joystick.Client;

internal sealed class JoystickClient(ILogger logger) {
    private const string CUserAgent = "SubLink JoystickClient/1.0";
    internal static readonly JsonSerializerOptions _deserializationOpt = new() {
        AllowOutOfOrderMetadataProperties = true
    };
    internal static readonly JsonSerializerOptions _serializationOpt = new() {
        WriteIndented = false,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    private readonly ILogger _logger = logger;
    private WebSocket? _socket;
    private OAuthClient? _authClient;

    public event EventHandler? OnJoystickConnected;
    public event EventHandler? OnJoystickDisconnected;
    public event EventHandler<JoystickErrorEventArgs>? OnJoystickError;
    public event EventHandler<JoystickChatMessageEventArgs>? OnJoystickChatMessage;
    public event EventHandler<JoystickBotMessageEventArgs>? OnJoystickBotMessage;
    public event EventHandler<JoystickEnterStreamEventArgs>? OnJoystickEnterStream;
    public event EventHandler<JoystickLeaveStreamEventArgs>? OnJoystickLeaveStream;
    public event EventHandler<JoystickStartedEventArgs>? OnJoystickStarted;
    public event EventHandler<JoystickChatTimersClearedEventArgs>? OnJoystickChatTimersCleared;


    public bool Enabled { get; internal set; } = false;

    public async Task<bool> ConnectAsync(JoystickSettings settings) {
        if (_socket != null)
            return true;

        _authClient = new(_logger, settings.OAuthPort, settings.ClientId, settings.ClientSecret,
            settings.AccessToken, settings.RefreshToken, settings.State);

        try {
            // Do some oauth bullshit
            await _authClient.AuthorizeUser();

            if (!_authClient.IsAuthenticated) {
                _logger.Information("[{TAG}] visit https://joystick.tv/applications to create a new bot, then fill in ApplicationID, ClientID and ClientSecret in {CONFIGFILE}",
                    Platform.PlatformName, Platform.PlatformConfigFile);
                return false;
            }

            string json = await System.IO.File.ReadAllTextAsync(Platform.PlatformConfigFile);
            JsonNode? j = JsonNode.Parse(json, documentOptions: new() { CommentHandling = JsonCommentHandling.Skip });

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            j[Platform.PlatformName]["AccessToken"] = _authClient.AccessToken;
            j[Platform.PlatformName]["RefreshToken"] = _authClient.RefreshToken;
            j[Platform.PlatformName]["State"] = _authClient.State;
            j[Platform.PlatformName]["Username"] = _authClient.Username;
            j[Platform.PlatformName]["ChannelId"] = _authClient.ChannelId;
            await System.IO.File.WriteAllTextAsync(Platform.PlatformConfigFile, j.ToJsonString(new() {
                WriteIndented = true,
                TypeInfoResolver = new DefaultJsonTypeInfoResolver()
            }));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            // Somehow Joystick now knows we are legit and can use the websocket API, magic
            _socket = new(
                $"wss://api.joystick.tv/cable?token={_authClient.AuthCode}",
                "actioncable-v1-json",
                version: WebSocketVersion.Rfc6455,
                userAgent: CUserAgent
            ) {
                EnableAutoSendPing = false,
                NoDelay = true
            };

            _socket.Opened += OnSockConnected;
            _socket.Closed += OnSockDisconnected;
            _socket.Error += OnSockError;
            _socket.MessageReceived += OnSockMessageReceived;
            _socket.DataReceived += OnSockDataReceived;

            await _socket.OpenAsync();
        } catch (Exception) {
            return false;
        }

        return true;
    }

    public async Task DisconnectAsync() {
        if (_socket == null) return;
        if (_socket.State != WebSocketState.Closed)
            await _socket.CloseAsync();

        _socket = null;
    }

    private void OnSockConnected(object? sender, EventArgs e) =>
        OnJoystickConnected?.Invoke(this, e);

    private void OnSockDisconnected(object? sender, EventArgs e) =>
        OnJoystickDisconnected?.Invoke(this, e);

    private void OnSockError(object? sender, ErrorEventArgs e) =>
        OnJoystickError?.Invoke(this, new(e.Exception));

    private void OnSockMessageReceived(object? sender, MessageReceivedEventArgs e) {
        var jsonObj = JsonDocument.Parse(e.Message, new JsonDocumentOptions { AllowTrailingCommas = true, CommentHandling = JsonCommentHandling.Skip });
        if (jsonObj == null) return;

        try {
            if (jsonObj.RootElement.TryGetProperty("type", out _)) {
                HandleResponseMessage(e.Message);
                return;
            }

            if (jsonObj.RootElement.TryGetProperty("identifier", out _)) {
                // Handle "message" events
                if (jsonObj.RootElement.TryGetProperty("message", out var msgObject) &&
                    msgObject.TryGetProperty("event", out var eventObject) &&
                    msgObject.TryGetProperty("type", out var typeObject)
                ) {
                    var messageJson = msgObject.GetRawText();

                    // Handle some specials that differ from the norm
                    if ("ChatMessage".Equals(eventObject.GetString(), StringComparison.OrdinalIgnoreCase) &&
                        "new_message".Equals(typeObject.GetString(), StringComparison.OrdinalIgnoreCase)
                    ) {
                        HandleChatMessage(messageJson);
                        return;
                    }

                    if ("BotMessage".Equals(eventObject.GetString(), StringComparison.OrdinalIgnoreCase) &&
                        "event_bot_message".Equals(typeObject.GetString(), StringComparison.OrdinalIgnoreCase)
                    ) {
                        HandleBotMessage(messageJson);
                        return;
                    }

                    // Handle "normal" events
                    HandleEventMessage(messageJson);
                    return;
                }

                // Place-holder I guess
            }

            _logger.Warning("[{TAG}] Unknown data received, message: {Message}", Platform.PlatformName, e.Message);
        } catch (Exception ex) {
            _logger.Error("[{TAG}] Exception raised while trying to handle JSON data:\nData: {Data}\nMessage: {Message}", Platform.PlatformName, e.Message, ex.ToString());
        }
    }

    private void OnSockDataReceived(object? sender, DataReceivedEventArgs e) =>
        _logger.Information("[{TAG}] Data received, length: {Length}", Platform.PlatformName, e.Data.Length);

    public void SendCommand(IBaseCommand cmd) {
        if (!Enabled) return;

        var jsonStr = JsonSerializer.Serialize(cmd, _serializationOpt);
        _logger.Information("[{TAG}] Sending: {jsonStr}", Platform.PlatformName, jsonStr);
        _socket?.Send(jsonStr);
    }

    public void SendString(string str) {
        if (!Enabled) return;

        _logger.Information("[{TAG}] Sending: {str}", Platform.PlatformName, str);
        _socket?.Send(str);
    }

    private void HandleResponseMessage(string message) {
        IBaseResponse? inMsg = JsonSerializer.Deserialize<IBaseResponse>(message, _deserializationOpt);
        if (inMsg == null) return;

        switch (inMsg) {
            case Welcome: {
                _logger.Information("[{TAG}] Welcome received", Platform.PlatformName);
                // We, SubLink, should only subscribe to the GatewayChannel
                SendCommand(new Subscribe());
                return;
            }
            case Ping: return; // Ignore, annoying and useless for anything other than keeping the socket alive
            case ConfirmSubscription: {
                ConfirmSubscription responseMsg = (ConfirmSubscription)inMsg;
                _logger.Information("[{TAG}] Confirmed subscription to event `{Channel}` for streamer `{StreamId}`",
                    Platform.PlatformName, responseMsg.Ident.Channel, responseMsg.Ident.StreamId);
                return;
            }
            case RejectSubscription: {
                RejectSubscription responseMsg = (RejectSubscription)inMsg;
                _logger.Information("[{TAG}] Rejected subscription to event `{Channel}` for streamer `{StreamId}`",
                    Platform.PlatformName, responseMsg.Ident.Channel, responseMsg.Ident.StreamId);
                return;
            }
            case ConfirmUnsubscription: {
                ConfirmUnsubscription responseMsg = (ConfirmUnsubscription)inMsg;
                _logger.Information("[{TAG}] Confirmed unsubscription to event `{Channel}` for streamer `{StreamId}`",
                    Platform.PlatformName, responseMsg.Ident.Channel, responseMsg.Ident.StreamId);
                return;
            }
            case RejectUnsubscription: {
                RejectUnsubscription responseMsg = (RejectUnsubscription)inMsg;
                _logger.Information("[{TAG}] Rejected unsubscription to event `{Channel}` for streamer `{StreamId}`",
                    Platform.PlatformName, responseMsg.Ident.Channel, responseMsg.Ident.StreamId);
                return;
            }
            default: {
                _logger.Warning("[{TAG}] Unknown data received, message: {Message}", Platform.PlatformName, message);
                return;
            }
        }
    }

    private void HandleChatMessage(string message) {
        var inMsg = JsonSerializer.Deserialize<NewMessageEvent>(message, _deserializationOpt);

        if (inMsg != null)
            OnJoystickChatMessage?.Invoke(this, new() {
                CreatedAt = inMsg.CreatedAt,
                Text = inMsg.Text,
                MessageId = inMsg.MessageId,
                Visibility = inMsg.Visibility,
                BotCommand = inMsg.BotCommand ?? string.Empty,
                BotCommandArg = inMsg.BotCommandArg ?? string.Empty,
                EmotesUsed = inMsg.EmotesUsed,
                AuthorSlug = inMsg.Author.Slug,
                AuthorUsername = inMsg.Author.Username,
                AuthorNickname = inMsg.Author.Nickname ?? string.Empty,
                AuthorIsStreamer = inMsg.Author.IsStreamer,
                AuthorIsModerator = inMsg.Author.IsModerator,
                AuthorIsSubscriber = inMsg.Author.IsSubscriber,
                AuthorIsVerified = inMsg.Author.IsVerified,
                AuthorIsContentCreator = inMsg.Author.IsContentCreator,
                StreamerSlug = inMsg.Streamer.Slug,
                StreamerUsername = inMsg.Streamer.Username,
                ChannelId = inMsg.ChannelId,
                Mention = inMsg.Mention,
                MentionedUsername = inMsg.MentionedUsername ?? string.Empty,
                Highlight = inMsg.Highlight
            });
    }

    private void HandleBotMessage(string message) {
        var inMsg = JsonSerializer.Deserialize<BotMessageEvent>(message, _deserializationOpt);

        if (inMsg != null)
            OnJoystickBotMessage?.Invoke(this, new()
            {
                CreatedAt = inMsg.CreatedAt,
                Text = inMsg.Text,
                MessageId = inMsg.MessageId,
                Visibility = inMsg.Visibility,
                EmotesUsed = inMsg.EmotesUsed,
                AuthorUsername = inMsg.Author.Username,
                Mention = inMsg.Mention
            });
    }

    private void HandleEventMessage(string message) {
        _logger.Warning("[{TAG}] HandleEventMessage, message: {Message}", Platform.PlatformName, message);
        IBaseEvent? inMsg = JsonSerializer.Deserialize<IBaseEvent>(message, _deserializationOpt);
        if (inMsg == null) return;

        switch (inMsg) {
            case EnterStreamEvent: {
                EnterStreamEvent eventMsg = (EnterStreamEvent)inMsg;
                OnJoystickEnterStream?.Invoke(this, new(eventMsg.Text, eventMsg.CreatedAt));
                return;
            }
            case LeaveStreamEvent: {
                LeaveStreamEvent eventMsg = (LeaveStreamEvent)inMsg;
                OnJoystickLeaveStream?.Invoke(this, new(eventMsg.Text, eventMsg.CreatedAt));
                return;
            }
            case StartedEvent: {
                StartedEvent eventMsg = (StartedEvent)inMsg;
                OnJoystickStarted?.Invoke(this, new(eventMsg.Text, eventMsg.CreatedAt, eventMsg.Metadata.Who));
                return;
            }
            case ChatTimersClearedEvent: {
                ChatTimersClearedEvent eventMsg = (ChatTimersClearedEvent)inMsg;
                OnJoystickChatTimersCleared?.Invoke(this, new(eventMsg.Text, eventMsg.CreatedAt, eventMsg.Metadata.Who));
                return;
            }
            default: {
                _logger.Warning("[{TAG}] Unknown data received, message: {Message}", Platform.PlatformName, message);
                return;
            }
        }
    }
}
