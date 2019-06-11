using Qmmands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using DiscordBotTemplates.Discord.Net.Commands;

namespace DiscordBotTemplates.Discord.Net.Services
{
    internal sealed class SampleEventHandler
    {
        private readonly ILogger<SampleEventHandler> _logger;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IConfiguration _config;
        private readonly IServiceProvider _services;

        public SampleEventHandler(ILogger<SampleEventHandler> logger, DiscordSocketClient client, CommandService commands, IConfiguration config, IServiceProvider services)
        {
            this._logger = logger;
            this._client = client;
            this._commands = commands;
            this._config = config;
            this._services = services;

            this._client.MessageReceived += CommandHandler;
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

            var context = new CommandContext(this._client, message);

            IResult res = await this._commands.ExecuteAsync(output, context, this._services);
            switch (res)
            {
                case FailedResult fRes:
                    this._logger.LogError("Command execution failed with reason: {0}", fRes.Reason);
                    break;
            }
        }
    }
}
