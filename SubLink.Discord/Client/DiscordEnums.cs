namespace tech.SubLink.Discord.Client;

public enum ChannelType { // Blame Discord docs: channel type (guild text: 0, guild voice: 2, dm: 1, group dm: 3)
    GuildText = 0,
    GuildVoice = 2,
    DM = 1,
    GroupDM = 3
}
