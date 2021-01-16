using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordBotTemplates.Discord.Net.Services
{
    internal sealed class DiscordBotService : IHostedService
    {
        private readonly ILogger<DiscordBotService> _logger;
        private readonly IConfiguration _config;
        private readonly IServiceProvider _services;
        private readonly DiscordSocketClient _client;

        public DiscordBotService(ILogger<DiscordBotService> logger, IConfiguration config, IServiceProvider services, DiscordSocketClient client)
        {
            _logger = logger;
            _config = config;
            _services = services;
            _client = client;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service starting up");

            _logger.LogInformation("Connecting the Discord client");

            #warning Ensure that this configuration variable is set.
            await _client.LoginAsync(TokenType.Bot, this._config["token"], true);

            await _client.StartAsync();

            _logger.LogInformation("Client started up successfully");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service stopping");

            using (_client)
            {
                await _client.StopAsync();

                await _client.LogoutAsync();
            }

            _logger.LogInformation("Services disposed, shutdown complete.");
        }
    }
}
