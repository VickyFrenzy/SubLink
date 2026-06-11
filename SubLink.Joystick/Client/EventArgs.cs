using System;

namespace xyz.yewnyx.SubLink.Joystick.Client;

internal sealed class JoystickErrorEventArgs : EventArgs {
    public Exception Exception { get; set; } = new();

    public JoystickErrorEventArgs() { }

    public JoystickErrorEventArgs(Exception exception) {
        Exception = exception;
    }
}

public class JoystickEventArgs : EventArgs {
    public string Text { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;

    public JoystickEventArgs() { }

    public JoystickEventArgs(string text, string createdAt) =>
        (Text, CreatedAt) = (text, createdAt);
}

public class JoystickChatMessageEventArgs : JoystickEventArgs {
    public string MessageId { get; set; } = string.Empty;
    public string Visibility { get; set; } = string.Empty;
    public string BotCommand {  get; set; } = string.Empty;
    public string BotCommandArg {  get; set; } = string.Empty;
    public string[] EmotesUsed { get; set; } = [];
    public string AuthorSlug { get; set; } = string.Empty;
    public string AuthorUsername { get; set; } = string.Empty;
    public string AuthorNickname { get; set; } = string.Empty;
    public bool AuthorIsStreamer { get; set; } = false;
    public bool AuthorIsModerator { get; set; } = false;
    public bool AuthorIsSubscriber { get; set; } = false;
    public bool AuthorIsVerified { get; set; } = false;
    public bool AuthorIsContentCreator { get; set; } = false;
    public string StreamerSlug { get; set; } = string.Empty;
    public string StreamerUsername { get; set; } = string.Empty;
    public string ChannelId { get; set; } = string.Empty;
    public bool Mention { get; set; } = false;
    public string MentionedUsername { get; set; } = string.Empty;
    public bool Highlight { get; set; } = false;

    public JoystickChatMessageEventArgs() { }
}

public class JoystickBotMessageEventArgs : JoystickEventArgs {
    public string MessageId { get; set; } = string.Empty;
    public string Visibility { get; set; } = string.Empty;
    public string[] EmotesUsed { get; set; } = [];
    public string AuthorUsername { get; set; } = string.Empty;
    public bool Mention { get; set; } = false;

    public JoystickBotMessageEventArgs() { }
}

public class JoystickEnterStreamEventArgs : EventArgs {
    public string Who { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;

    public JoystickEnterStreamEventArgs() { }

    public JoystickEnterStreamEventArgs(string who, string createdAt) =>
        (Who, CreatedAt) = (who, createdAt);
}

public class JoystickLeaveStreamEventArgs : EventArgs {
    public string Who { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;

    public JoystickLeaveStreamEventArgs() { }

    public JoystickLeaveStreamEventArgs(string who, string createdAt) =>
        (Who, CreatedAt) = (who, createdAt);
}

public class JoystickStartedEventArgs : JoystickEventArgs {
    public string Who { get; set; } = string.Empty;

    public JoystickStartedEventArgs() { }

    public JoystickStartedEventArgs(string text, string createdAt, string who) =>
        (Text, CreatedAt, Who) = (text, createdAt, who);
}

public class JoystickChatTimersClearedEventArgs : JoystickEventArgs {
    public string Who { get; set; } = string.Empty;

    public JoystickChatTimersClearedEventArgs() { }

    public JoystickChatTimersClearedEventArgs(string text, string createdAt, string who) =>
        (Text, CreatedAt, Who) = (text, createdAt, who);
}
