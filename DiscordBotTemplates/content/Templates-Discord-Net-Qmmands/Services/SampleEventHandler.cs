using Qmmands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using DiscordBotTemplates.Discord.Net.Commands;
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
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Hooking events");
            _client.MessageReceived += CommandHandler;
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Unhooking events");
            _client.MessageReceived -= CommandHandler;
            return Task.CompletedTask;
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
            if (!CommandUtilities.HasPrefix(message.Content, _config["prefix"], out var output))
            {
                return;
            }

            var context = new DiscordCommandContext(_client, message, _services);

            IResult res = await _commands.ExecuteAsync(output, context);
            switch (res)
            {
                case FailedResult fRes:
                    _logger.LogError("Command execution failed with reason: {0}", fRes.Reason);
                    break;
            }
        }
    }
}
