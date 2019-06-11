using DSharpPlus;
using DSharpPlus.EventArgs;
using System;
using System.Threading.Tasks;

namespace DiscordBotTemplates.DSharpPlus.Services
{
    internal sealed class SampleEventHandler
    {
        private readonly DiscordClient _client;

        public SampleEventHandler(DiscordClient client)
        {
            this._client = client;

            this._client.MessageCreated += PingHandler;
        }

        private Task PingHandler(MessageCreateEventArgs eventArgs)
        {
            if (eventArgs.Message.Content.Equals("!ping", StringComparison.OrdinalIgnoreCase))
            {
                eventArgs.Channel.SendMessageAsync("Pong!");
            }

            return Task.CompletedTask;
        }
    }
}
