using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace content.Services
{
    internal sealed class SampleEventHandler
    {
        private readonly DiscordSocketClient _client;

        public SampleEventHandler(DiscordSocketClient client)
        {
            this._client = client;

            this._client.MessageReceived += PingHandler;
        }

        private Task PingHandler(SocketMessage msg)
        {
            if (msg.Content.Equals("!ping", StringComparison.OrdinalIgnoreCase))
            {
                return msg.Channel.SendMessageAsync("Pong!");
            }

            return Task.CompletedTask;
        }
    }
}
