using System;
using System.Threading.Tasks;
using xyz.yewnyx.SubLink.Joystick.Client;

namespace xyz.yewnyx.SubLink.Joystick.Services;

internal sealed partial class JoystickService {
    private void WireCallbacks() {
        _client.OnJoystickConnected += OnJoystickConnected;
        _client.OnJoystickDisconnected += OnJoystickDisconnected;
        _client.OnJoystickError += OnJoystickError;
        _client.OnJoystickChatMessage += OnJoystickChatMessage;
        _client.OnJoystickBotMessage += OnJoystickBotMessage;
        _client.OnJoystickEnterStream += OnJoystickEnterStream;
        _client.OnJoystickLeaveStream += OnJoystickLeaveStream;
        _client.OnJoystickStarted += OnJoystickStarted;
        _client.OnJoystickChatTimersCleared += OnJoystickChatTimersCleared;
    }

    private void OnJoystickConnected(object? sender, EventArgs e) =>
        _logger.Information("[{TAG}] Connected to websocket", Platform.PlatformName);

    private void OnJoystickDisconnected(object? sender, EventArgs e) =>
        _logger.Warning("[{TAG}] Disconnected from websocket", Platform.PlatformName);

    private void OnJoystickError(object? sender, JoystickErrorEventArgs e) =>
        _logger.Error("[{TAG}] Error ocured with the websocket\r\n{Exception}", Platform.PlatformName, e.Exception);

    private void OnJoystickChatMessage(object? sender, JoystickChatMessageEventArgs e) {
        Task.Run(async () => {
            if (_rules is JoystickRules { OnJoystickChatMessage: { } callback })
                await callback(e);
        });
    }

    private void OnJoystickBotMessage(object? sender, JoystickBotMessageEventArgs e) {
        Task.Run(async () => {
            if (_rules is JoystickRules { OnJoystickBotMessage: { } callback })
                await callback(e);
        });
    }

    private void OnJoystickEnterStream(object? sender, JoystickEnterStreamEventArgs e) {
        Task.Run(async () => {
            if (_rules is JoystickRules { OnJoystickEnterStream: { } callback })
                await callback(e);
        });
    }

    private void OnJoystickLeaveStream(object? sender, JoystickLeaveStreamEventArgs e) {
        Task.Run(async () => {
            if (_rules is JoystickRules { OnJoystickLeaveStream: { } callback })
                await callback(e);
        });
    }

    private void OnJoystickStarted(object? sender, JoystickStartedEventArgs e) {
        Task.Run(async () => {
            if (_rules is JoystickRules { OnJoystickStarted: { } callback })
                await callback(e);
        });
    }

    private void OnJoystickChatTimersCleared(object? sender, JoystickChatTimersClearedEventArgs e) {
        Task.Run(async () => {
            if (_rules is JoystickRules { OnJoystickChatTimersCleared: { } callback })
                await callback(e);
        });
    }
}
