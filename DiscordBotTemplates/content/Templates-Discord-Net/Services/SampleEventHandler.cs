using Discord.WebSocket;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DiscordBotTemplates.Discord.Net.Services
{
    internal sealed class SampleEventHandler : IHostedService
    {
        private readonly DiscordSocketClient _client;
        private readonly ILogger<SampleEventHandler> _logger;

        public SampleEventHandler(DiscordSocketClient client, ILogger<SampleEventHandler> logger)
        {
            _client = client;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Hooking event handlers");
            _client.MessageReceived += PingHandler;
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Unhooking event handlers");
            _client.MessageReceived -= PingHandler;
            return Task.CompletedTask;
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
