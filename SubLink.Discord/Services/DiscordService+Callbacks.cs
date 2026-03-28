using System;
using System.Threading.Tasks;
using xyz.yewnyx.SubLink.Discord.Client;

namespace xyz.yewnyx.SubLink.Discord.Services;

internal sealed partial class DiscordService {
    private void WireCallbacks() {
        _discord.OnReady += OnReady;
        _discord.OnError += OnError;
        _discord.OnGuildStatus += OnGuildStatus;
        _discord.OnGuildCreate += OnGuildCreate;
        _discord.OnChannelCreate += OnChannelCreate;
        _discord.OnSelectedVoiceChannel += OnSelectedVoiceChannel;
        _discord.OnVoiceSettingsUpdate += OnVoiceSettingsUpdate;
        _discord.OnVoiceStateCreate += OnVoiceStateCreate;
        _discord.OnVoiceStateUpdate += OnVoiceStateUpdate;
        _discord.OnVoiceStateDelete += OnVoiceStateDelete;
        _discord.OnVoiceStatusUpdate += OnVoiceStatusUpdate;
        _discord.OnMessageCreate += OnMessageCreate;
        _discord.OnMessageUpdate += OnMessageUpdate;
        _discord.OnMessageDelete += OnMessageDelete;
        _discord.OnStartSpeaking += OnStartSpeaking;
        _discord.OnStopSpeaking += OnStopSpeaking;
        _discord.OnNotificationCreate += OnNotificationCreate;
        _discord.OnActivityJoin += OnActivityJoin;
        _discord.OnActivitySpectate += OnActivitySpectate;
        _discord.OnActivityJoinRequest += OnActivityJoinRequest;
    }

    private void OnReady(object? sender, EventArgs e) =>
        Task.Run(async () => {
            if (_rules is DiscordRules { OnReady: { } callback })
                await callback();
        });

    private void OnError(object? sender, DiscordErrorArgs e) =>
        Task.Run(async () => {
            if (_rules is DiscordRules { OnError: { } callback })
                await callback(e);
        });

    private void OnGuildStatus(object? sender, DiscordGuildInfoEventArgs e) =>
        Task.Run(async () => {
            if (_rules is DiscordRules { OnGuildStatus: { } callback })
                await callback(e);
        });

    private void OnGuildCreate(object? sender, DiscordGuildInfoEventArgs e) =>
        Task.Run(async () => {
            if (_rules is DiscordRules { OnGuildCreate: { } callback })
                await callback(e);
        });

    private void OnChannelCreate(object? sender, DiscordChannelCreateEventArgs e) =>
        Task.Run(async () => {
            if (_rules is DiscordRules { OnChannelCreate: { } callback })
                await callback(e);
        });

    private void OnSelectedVoiceChannel(object? sender, DiscordVoiceChannelIdEventArgs e) =>
        Task.Run(async () => {
            if (_rules is DiscordRules { OnSelectedVoiceChannel: { } callback })
                await callback(e);
        });

    private void OnVoiceSettingsUpdate(object? sender, DiscordVoiceSettingsEventArgs e) =>
        Task.Run(async () => {
            if (_rules is DiscordRules { OnVoiceSettingsUpdate: { } callback })
                await callback(e);
        });

    private void OnVoiceStateCreate(object? sender, DiscordVoiceStateEventArgs e) =>
        Task.Run(async () => {
            if (_rules is DiscordRules { OnVoiceStateCreate: { } callback })
                await callback(e);
        });

    private void OnVoiceStateUpdate(object? sender, DiscordVoiceStateEventArgs e) =>
        Task.Run(async () => {
            if (_rules is DiscordRules { OnVoiceStateUpdate: { } callback })
                await callback(e);
        });

    private void OnVoiceStateDelete(object? sender, DiscordVoiceStateEventArgs e) =>
        Task.Run(async () => {
            if (_rules is DiscordRules { OnVoiceStateDelete: { } callback })
                await callback(e);
        });

    private void OnVoiceStatusUpdate(object? sender, DiscordVoiceStatusEventArgs e) =>
        Task.Run(async () => {
            if (_rules is DiscordRules { OnVoiceStatusUpdate: { } callback })
                await callback(e);
        });

    private void OnMessageCreate(object? sender, DiscordMessageEventArgs e) =>
        Task.Run(async () => {
            if (_rules is DiscordRules { OnMessageCreate: { } callback })
                await callback(e);
        });

    private void OnMessageUpdate(object? sender, DiscordMessageEventArgs e) =>
        Task.Run(async () => {
            if (_rules is DiscordRules { OnMessageUpdate: { } callback })
                await callback(e);
        });

    private void OnMessageDelete(object? sender, DiscordMessageEventArgs e) =>
        Task.Run(async () => {
            if (_rules is DiscordRules { OnMessageDelete: { } callback })
                await callback(e);
        });

    private void OnStartSpeaking(object? sender, DiscordUserIdEventArgs e) =>
        Task.Run(async () => {
            if (_rules is DiscordRules { OnStartSpeaking: { } callback })
                await callback(e.UserId);
        });

    private void OnStopSpeaking(object? sender, DiscordUserIdEventArgs e) =>
        Task.Run(async () => {
            if (_rules is DiscordRules { OnStopSpeaking: { } callback })
                await callback(e.UserId);
        });

    private void OnNotificationCreate(object? sender, DiscordNotificationEventArgs e) =>
        Task.Run(async () => {
            if (_rules is DiscordRules { OnNotificationCreate: { } callback })
                await callback(e);
        });

    private void OnActivityJoin(object? sender, EventArgs e) =>
        Task.Run(async () => {
            if (_rules is DiscordRules { OnActivityJoin: { } callback })
                await callback();
        });

    private void OnActivitySpectate(object? sender, EventArgs e) =>
        Task.Run(async () => {
            if (_rules is DiscordRules { OnActivitySpectate: { } callback })
                await callback();
        });

    private void OnActivityJoinRequest(object? sender, DiscordUserIdEventArgs e) =>
        Task.Run(async () => {
            if (_rules is DiscordRules { OnActivityJoinRequest: { } callback })
                await callback(e.UserId);
        });
}
