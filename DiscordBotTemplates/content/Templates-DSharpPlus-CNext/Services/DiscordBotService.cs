using DSharpPlus;
using DSharpPlus.CommandsNext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
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

        public DiscordBotService(ILogger<DiscordBotService> logger, 
            IConfiguration config, 
            IServiceProvider services, 
            DiscordClient client)
        {
            this._logger = logger;
            this._config = config;
            this._services = services;
            this._client = client;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            this._logger.LogInformation("Service starting up");

            this._logger.LogInformation("Connecting the Discord client");

            await this._client.ConnectAsync();

            this._logger.LogInformation("Client started up successfully");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            this._logger.LogInformation("Service stopping");

            using (this._client)
            {
                await this._client.DisconnectAsync();
            }

            this._logger.LogInformation("Services disposed, shutdown complete.");
        }
    }
}
