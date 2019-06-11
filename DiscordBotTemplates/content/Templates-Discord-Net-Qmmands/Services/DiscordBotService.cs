﻿using Qmmands;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
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

        public DiscordBotService(ILogger<DiscordBotService> logger, IConfiguration config, IServiceProvider services, DiscordSocketClient client, CommandService commands, /* See comment */ SampleEventHandler eventHandler)
        {
            this._logger = logger;
            this._config = config;
            this._services = services;
            this._client = client;
            this._commands = commands;

            this._client.Log += HandleLog;

            // When it is injected, the IOC container calls the constructor of SampleEventHandler, which registers its hooks with the DiscordSocketClient.
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            this._logger.LogInformation("Service starting up");

            #warning If your commands are in a different assembly, you should edit this line appropriately.
            var modules = this._commands.AddModules(Assembly.GetEntryAssembly());

            this._logger.LogInformation("Registered {0} commands in {1} modules",
                this._commands.GetAllCommands().Count,
                modules.Count);

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

        private Task HandleLog(LogMessage msg)
        {
            switch (msg.Severity)
            {
                case LogSeverity.Critical:
                    this._logger.LogCritical(msg.Message);
                    break;
                case LogSeverity.Error:
                    this._logger.LogError(msg.Message);
                    break;
                case LogSeverity.Warning:
                    this._logger.LogWarning(msg.Message);
                    break;
                case LogSeverity.Info:
                    this._logger.LogInformation(msg.Message);
                    break;
                case LogSeverity.Verbose:
                    this._logger.LogDebug(msg.Message);
                    break;
                case LogSeverity.Debug:
                    this._logger.LogTrace(msg.Message);
                    break;
            }

            return Task.CompletedTask;
        }
    }
}