using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
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
        private readonly CommandService _commands;

        public DiscordBotService(ILogger<DiscordBotService> logger, IConfiguration config, IServiceProvider services, DiscordSocketClient client, CommandService commands)
        {
            _logger = logger;
            _config = config;
            _services = services;
            _client = client;
            _commands = commands;

            _client.Log += HandleLog;
            _commands.Log += HandleLog;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service starting up");

            #warning If your commands are in a different assembly, you should edit this line appropriately.
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

            _logger.LogInformation("Registered {0} commands in {1} modules", 
                _commands.Commands.Count(), 
                _commands.Modules.Count());

            _logger.LogInformation("Connecting the Discord client");

            #warning Ensure that this configuration variable is set.
            await _client.LoginAsync(TokenType.Bot, _config["token"]);

            await _client.StartAsync();

            _logger.LogInformation("Client started up successfully");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service stopping");

            using (_client)
            using (_commands)
            {
                await _client.StopAsync();

                await _client.LogoutAsync();
            }

            _logger.LogInformation("Services disposed, shutdown complete.");
        }

        private Task HandleLog(LogMessage msg)
        {
            switch (msg.Severity)
            {
                case LogSeverity.Critical:
                    _logger.LogCritical(msg.Message);
                    break;
                case LogSeverity.Error:
                    _logger.LogError(msg.Message);
                    break;
                case LogSeverity.Warning:
                    _logger.LogWarning(msg.Message);
                    break;
                case LogSeverity.Info:
                    _logger.LogInformation(msg.Message);
                    break;
                case LogSeverity.Verbose:
                    _logger.LogDebug(msg.Message);
                    break;
                case LogSeverity.Debug:
                    _logger.LogTrace(msg.Message);
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
