using System.Text.Json.Serialization;

namespace tech.SubLink.Kick.KickClient.Events;

public sealed class ChatroomClearEvent {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
}
