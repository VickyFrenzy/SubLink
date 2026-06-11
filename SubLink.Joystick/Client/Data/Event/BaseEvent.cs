using System.Text.Json.Serialization;

namespace xyz.yewnyx.SubLink.Joystick.Client.Data.Event;

/*
Types:
- StreamResuming (event = StreamEvent)
- StreamEnding (event = StreamEvent)
- Ended (event = StreamEvent)

- Followed (event = StreamEvent)
- FollowerCountUpdated (event = StreamEvent)

- Tipped (event = StreamEvent)
- TipGoalCreated (event = StreamEvent)
- TipGoalDeleted (event = StreamEvent)
- TipGoalUpdated (event = StreamEvent)
- TipGoalIncreased (event = StreamEvent)
- TipGoalMet (event = StreamEvent)
- TipMenuItemLocked (event = StreamEvent)
- TipMenuItemUnlocked (event = StreamEvent)

- ChatTimerStarted (event = StreamEvent)

- DropinStream (event = StreamEvent)
- StreamDroppedIn (event = StreamEvent)

- Subscribed (event = StreamEvent)
- Resubscribed (event = StreamEvent)
- GiftedSubscriptions (event = StreamEvent)

- WheelSpinClaimed (event = StreamEvent)
- ViewerCountUpdated (event = StreamEvent)
- SubscriberCountUpdated (event = StreamEvent)
- MilestoneCompleted (event = StreamEvent)

- PvpSessionRequested (event = StreamEvent)
- PvpSessionReady (event = StreamEvent)
- PvpSessionStarted (event = StreamEvent)
- PvpSessionEnding (event = StreamEvent)
- PvpSessionEnded (event = StreamEvent)

- SceneUpdated (event = StreamEvent)
- SettingsUpdated (event = StreamEvent)
- StreamModeUpdated (event = StreamEvent)

- UserMuted (event = StreamEvent)
- UserUnmuted (event = StreamEvent)

- DeviceConnected (event = StreamEvent)
- DeviceDisconnected (event = StreamEvent)
- DeviceSettingsUpdated (event = StreamEvent)

- ChatMessageReceived (event = StreamEvent)
*/
[JsonPolymorphic(
    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor,
    TypeDiscriminatorPropertyName = "type"
)]
[JsonDerivedType(typeof(EnterStreamEvent), "enter_stream")]
[JsonDerivedType(typeof(LeaveStreamEvent), "leave_stream")]
[JsonDerivedType(typeof(StartedEvent), "Started")]
[JsonDerivedType(typeof(ChatTimersClearedEvent), "ChatTimersCleared")]
public interface IBaseEvent {
    [JsonPropertyName("id")]
    string Id { get; set; }
    [JsonPropertyName("event")]
    string Event { get; set; }
    [JsonPropertyName("text")]
    string Text { get; set; }
    [JsonPropertyName("channelId")]
    string ChannelId { get; set; }
    [JsonPropertyName("createdAt")]
    string CreatedAt { get; set; }
    [JsonPropertyName("metadata")]
    string JsonMetaObj { get; set; }
}

public abstract class  BaseMessage : IBaseEvent {
    public string Id { get; set; } = string.Empty;
    public string Event { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string ChannelId { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
    internal string _metadataStr = string.Empty;
    public string JsonMetaObj {
        get {
            GetMetadata();
            return _metadataStr;
        }
        set {
            _metadataStr = value;
            SetMetadata();
        }
    }

    internal virtual void SetMetadata() { }
    internal virtual void GetMetadata() { }
}
