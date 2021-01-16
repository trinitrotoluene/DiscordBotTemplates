using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace DiscordBotTemplates.Discord.Net.Services
{
    internal sealed class SampleEventHandler : IHostedService
    {
        private readonly ILogger<SampleEventHandler> _logger;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IConfiguration _config;
        private readonly IServiceProvider _services;

        public SampleEventHandler(ILogger<SampleEventHandler> logger, DiscordSocketClient client, CommandService commands, IConfiguration config, IServiceProvider services)
        {
            _logger = logger;
            _client = client;
            _commands = commands;
            _config = config;
            _services = services;
        }

        private async Task CommandHandler(SocketMessage msg)
        {
            // Do not respond to bot accounts or system messages.
            if (!(msg is SocketUserMessage message) || msg.Author.IsBot)
            {
                return;
            }

            // Respond to both mentions and your prefix.
            #warning Ensure that this configuration variable is set.
            int argPos = 0;
            if (message.HasStringPrefix(_config["prefix"], ref argPos)
                || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                IResult result = null;
                try
                {
                    var context = new SocketCommandContext(_client, message);
                    result = await _commands.ExecuteAsync(context, argPos, _services);
                }
                finally
                {
                    if (!result?.IsSuccess ?? false)
                    {
                        _logger.LogError("Error Type: {0}{1}Error Reason: {2}", result.Error, Environment.NewLine, result.ErrorReason);
                    }
                }
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Hooking events");
            _client.MessageReceived += CommandHandler;
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Unhooking events");
            return Task.CompletedTask;
        }
    }
}
