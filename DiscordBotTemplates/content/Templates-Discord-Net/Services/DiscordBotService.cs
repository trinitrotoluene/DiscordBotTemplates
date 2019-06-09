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

        public DiscordBotService(ILogger<DiscordBotService> logger, IConfiguration config, IServiceProvider services, DiscordSocketClient client, /* See comment */ SampleEventHandler eventHandler)
        {
            this._logger = logger;
            this._config = config;
            this._services = services;
            this._client = client;

            // When it is injected, the IOC container calls the constructor of SampleEventHandler, which registers its hooks with the DiscordSocketClient.
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            this._logger.LogInformation("Service starting up");

            this._logger.LogInformation("Connecting the Discord client");

            #warning Ensure that this configuration variable is set.
            await this._client.LoginAsync(TokenType.Bot, this._config["token"], true);

            await this._client.StartAsync();

            this._logger.LogInformation("Client started up successfully");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            this._logger.LogInformation("Service stopping");

            using (this._client)
            {
                await this._client.StopAsync();

                await this._client.LogoutAsync();
            }

            this._logger.LogInformation("Services disposed, shutdown complete.");
        }
    }
}
