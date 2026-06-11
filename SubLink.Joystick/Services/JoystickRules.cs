using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using xyz.yewnyx.SubLink.Joystick.Client;
using xyz.yewnyx.SubLink.Platforms;

namespace xyz.yewnyx.SubLink.Joystick.Services;

[PublicAPI]
public sealed class JoystickRules : IPlatformRules {
    private JoystickService? _service;

    internal Func<JoystickChatMessageEventArgs, Task>? OnJoystickChatMessage;
    internal Func<JoystickBotMessageEventArgs, Task>? OnJoystickBotMessage;
    internal Func<JoystickEnterStreamEventArgs, Task>? OnJoystickEnterStream;
    internal Func<JoystickLeaveStreamEventArgs, Task>? OnJoystickLeaveStream;
    internal Func<JoystickStartedEventArgs, Task>? OnJoystickStarted;
    internal Func<JoystickChatTimersClearedEventArgs, Task>? OnJoystickChatTimersCleared;

    internal void SetService(JoystickService service) {
        _service = service;
    }

    /* Reacts */
    public void ReactToChatMessage(Func<JoystickChatMessageEventArgs, Task> with) { OnJoystickChatMessage = with; }
    public void ReactToBotMessage(Func<JoystickBotMessageEventArgs, Task> with) { OnJoystickBotMessage = with; }
    public void ReactToEnterStream(Func<JoystickEnterStreamEventArgs, Task> with) { OnJoystickEnterStream = with; }
    public void ReactToLeaveStream(Func<JoystickLeaveStreamEventArgs, Task> with) { OnJoystickLeaveStream = with; }
    public void ReactToStarted(Func<JoystickStartedEventArgs, Task> with) { OnJoystickStarted = with; }
    public void ReactToChatTimersCleared(Func<JoystickChatTimersClearedEventArgs, Task> with) { OnJoystickChatTimersCleared = with; }

    /* Actions */
    public void ChatMessage(string text) {
        if (_service == null || string.IsNullOrWhiteSpace(text)) return;
        _service.SendChatMessage(text);
    }

    public void DeleteMessage(string messageId) {
        if (_service == null || string.IsNullOrWhiteSpace(messageId)) return;
        _service.SendDeleteMessage(messageId);
    }

    public void Whisper(string username, string text) {
        if (_service == null || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(text)) return;
        _service.SendWhisper(username, text);
    }

    public void MuteUser(string messageId) {
        if (_service == null || string.IsNullOrWhiteSpace(messageId)) return;
        _service.SendChatMessage(messageId);
    }

    public void UnmuteUser(string username) {
        if (_service == null || string.IsNullOrWhiteSpace(username)) return;
        _service.SendUnmuteUser(username);
    }

    public void BlockUser(string messageId) {
        if (_service == null || string.IsNullOrWhiteSpace(messageId)) return;
        _service.SendBlockUser(messageId);
    }
}
