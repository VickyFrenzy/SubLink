# SubLink DataTypes Discord Types

[Back To Readme](../../../README.md)  
[Back To Discord DataTypes Index](Index.md)

## ChannelType
Blame Discord docs: channel type (guild text: 0, guild voice: 2, dm: 1, group dm: 3)

- GuildText  - `0`
- GuildVoice - `2`
- DM         - `1`
- GroupDM    - `3`

## DiscordErrorArgs

- `int`    Code    - Error code
- `string` Message - Error message

## DiscordGuildInfoEventArgs

- `string` Id      - Guild ID
- `string` Name    - Guild Name
- `string` IconUrl - Guild Icon URL

## DiscordChannelCreateEventArgs

- `string`      Id   - Discord channel ID
- `string`      Name - Discord channel name
- `ChannelType` Type - Discord channel type

## DiscordVoiceChannelIdEventArgs

- `string` ChannelId - Discord channel ID
- `string` GuildId   - Discord Guild ID

## DiscordVoiceSettingsEventArgs

- `float`  InputVolume       - Current voice input volume
- `float`  OutputVolume      - Current voice output volume
- `string` ModeType          - Voice mode type (VOICE_ACTIVITY or PUSH_TO_TALK)
- `bool`   ModeAutoThreshold - Indicates if the Voice Activity input threshold is automated or manual
- `float`  ModeThreshold     - Voice Activity input threshold
- `float`  ModeDelay         - Push-to-talk release delay
- `bool`   AutoGainControl   - Indicates if Automatic voice gain is active
- `bool`   EchoCancelation   - Indicates if Echo cancelation is active
- `bool`   Qos               - Indicates if Voice Qos is active
- `bool`   SilenceWarning    - Indicates if Discord will warn you when it's not detecting mic audio
- `bool`   Deaf              - Indicates if you are deafened
- `bool`   Mute              - Indicates if you ate muted

## DiscordVoiceStateEventArgs
Most of these are guesses, the docs severely lack info here

- `string` Id                - User ID
- `string` Username          - User username
- `string` Nickname          - User nickname
- `bool`   IsBot             - Indicated if the user is a bot
- `int`    Volume            - User volume override
- `bool`   Muted             - Indicates if the user is muted via override
- `bool`   StateMuted        - Indicates if the user is server muted
- `bool`   StateDeadened     - Indicates if the user is server deafened
- `bool`   StateSelfMuted    - Indicates if the user is muted
- `bool`   StateSelfDeadened - Indicates if the user is deafened
- `bool`   StateSuppressed   - Indicates if the user is suppressed

## DiscordVoiceStatusEventArgs

- `string` State     - Textual representation of the voice state
- `int`    StateCode - Numerical representation of the voice state

## DiscordMessageEventArgs

- `string` ChannelId       - Message channel ID
- `string` MessageId       - Message ID
- `bool`   IsBlocked       - Indicates if the message is from a blocked user
- `string` Content         - Message content
- `string` Timestamp       - Message Timestamp
- `string` EditedTimestamp - Message Last Edited Timestamp
- `bool`   IsPinned        - Indicates if the message is pinned
- `string` UserId          - Message author user ID
- `string` Username        - Message author username
- `bool`   IsBot           - Indicates if the message author is a bot

## DiscordNotificationEventArgs

- `string`                  ChannelId - Notification channel ID
- `string`                  Title     - Notification title
- `string`                  Body      - Notification body
- `string`                  IconUrl   - Notification icon URL
- `DiscordMessageEventArgs` Message   - Notification message
