using DSharpPlus;
using DSharpPlus.EventArgs;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DiscordBotTemplates.DSharpPlus.Services
{
    internal sealed class SampleEventHandler : IHostedService
    {
        private readonly DiscordClient _client;
        private readonly ILogger<SampleEventHandler> _logger;

        public SampleEventHandler(DiscordClient client, ILogger<SampleEventHandler> logger)
        {
            _client = client;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Unhooking events");
            _client.MessageCreated += PingHandler;
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Unhooking events");
            _client.MessageCreated -= PingHandler;
            return Task.CompletedTask;
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
