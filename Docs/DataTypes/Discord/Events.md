# SubLink DataTypes Discord Events

[Back To Readme](../../../README.md)  
[Back To Discord DataTypes Index](Index.md)

## ReactToReady

- Parameter type Nothing
- Boilerplate
```csharp
discord.ReactToReady(async () => {
    // Your Code
});
```

## ReactToError

- Parameter type `DiscordErrorArgs`
- Boilerplate
```csharp
discord.ReactToError(async error => {
    // Your Code
});
```

## ReactToGuildStatus

- Parameter type `DiscordGuildStatusEventArgs`
- Boilerplate
```csharp
discord.ReactToGuildStatus(async guild => {
    // Your Code
});
```

## ReactToGuildCreate

- Parameter type `DiscordGuildIdNameEventArgs`
- Boilerplate
```csharp
discord.ReactToGuildCreate(async guild => {
    // Your Code
});
```

## ReactToChannelCreate

- Parameter type `DiscordChannelCreateEventArgs`
- Boilerplate
```csharp
discord.ReactToChannelCreate(async channel => {
    // Your Code
});
```

## ReactToSelectedVoiceChannel

- Parameter type `DiscordVoiceChannelIdEventArgs`
- Boilerplate
```csharp
discord.ReactToSelectedVoiceChannel(async voiceChannel => {
    // Your Code
});
```

## ReactToVoiceSettingsUpdate

- Parameter type `DiscordVoiceSettingsEventArgs`
- Boilerplate
```csharp
discord.ReactToVoiceSettingsUpdate(async voiceSettings => {
    // Your Code
});
```

## ReactToVoiceStateCreate

- Parameter type `DiscordVoiceStateEventArgs`
- Boilerplate
```csharp
discord.ReactToVoiceStateCreate(async voiceState => {
    // Your Code
});
```

## ReactToVoiceStateUpdate

- Parameter type `DiscordVoiceStateEventArgs`
- Boilerplate
```csharp
discord.ReactToVoiceStateUpdate(async voiceState => {
    // Your Code
});
```

## ReactToVoiceStateDelete

- Parameter type `DiscordVoiceStateEventArgs`
- Boilerplate
```csharp
discord.ReactToVoiceStateDelete(async voiceState => {
    // Your Code
});
```

## ReactToVoiceStatusUpdate

- Parameter type `DiscordVoiceStatusEventArgs`
- Boilerplate
```csharp
discord.ReactToVoiceStatusUpdate(async voiceStatus => {
    // Your Code
});
```

## ReactToMessageCreate

- Parameter type `DiscordMessageEventArgs`
- Boilerplate
```csharp
discord.ReactToMessageCreate(async message => {
    // Your Code
});
```

## ReactToMessageUpdate

- Parameter type `DiscordMessageEventArgs`
- Boilerplate
```csharp
discord.ReactToMessageUpdate(async message => {
    // Your Code
});
```

## ReactToMessageDelete

- Parameter type `DiscordMessageEventArgs`
- Boilerplate
```csharp
discord.ReactToMessageDelete(async message => {
    // Your Code
});
```

## ReactToStartSpeaking

- Parameter type `string`
- Boilerplate
```csharp
discord.ReactToStartSpeaking(async userId => {
    // Your Code
});
```

## ReactToStopSpeaking

- Parameter type `string`
- Boilerplate
```csharp
discord.ReactToStopSpeaking(async userId => {
    // Your Code
});
```

## ReactToNotificationCreate

- Parameter type `DiscordNotificationEventArgs`
- Boilerplate
```csharp
discord.ReactToNotificationCreate(async notification => {
    // Your Code
});
```

## ReactToActivityJoin

- Parameter type Nothing
- Boilerplate
```csharp
discord.ReactToActivityJoin(async () => {
    // Your Code
});
```

## ReactToActivitySpectate

- Parameter type Nothing
- Boilerplate
```csharp
discord.ReactToActivitySpectate(async () => {
    // Your Code
});
```

## ReactToActivityJoinRequest

- Parameter type `string`
- Boilerplate
```csharp
discord.ReactToActivityJoinRequest(async userId => {
    // Your Code
});
```
