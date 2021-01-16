using DSharpPlus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordBotTemplates.DSharpPlus.Services
{
    internal sealed class DiscordBotService : IHostedService
    {
        private readonly ILogger<DiscordBotService> _logger;
        private readonly IConfiguration _config;
        private readonly IServiceProvider _services;
        private readonly DiscordClient _client;

        public DiscordBotService(ILogger<DiscordBotService> logger, IConfiguration config, IServiceProvider services, DiscordClient client)
        {
            _logger = logger;
            _config = config;
            _services = services;
            _client = client;

            // When it is injected, the IOC container calls the constructor of SampleEventHandler, which registers its hooks with the DiscordSocketClient.
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service starting up");

            _logger.LogInformation("Connecting the Discord client");

            await _client.ConnectAsync();

            _logger.LogInformation("Client started up successfully");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service stopping");

            using (_client)
            {
                await _client.DisconnectAsync();
            }

            _logger.LogInformation("Services disposed, shutdown complete.");
        }
    }
}
