using Discord.WebSocket;
using Qmmands;

namespace DiscordBotTemplates.Discord.Net.Commands
{
    public class CommandContext : ICommandContext
    {
        public DiscordSocketClient Client { get; }

        public SocketGuild Guild { get; }

        public ISocketMessageChannel Channel { get; }

        public SocketUserMessage Message { get; }

        public SocketUser User { get; }

        public SocketGuildUser Member { get; }

        public CommandContext(DiscordSocketClient client, SocketUserMessage message)
        {
            this.Client = client;
            this.Message = message;
            this.Channel = message.Channel;
            this.Guild = (this.Channel as SocketGuildChannel)?.Guild;
            this.User = message.Author;
            this.Member = message.Author as SocketGuildUser;
        }
    }
}
