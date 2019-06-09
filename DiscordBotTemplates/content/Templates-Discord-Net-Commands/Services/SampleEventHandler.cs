using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

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
            int argPos = 0;
            if (message.HasStringPrefix(this._config["prefix"], ref argPos)
                || message.HasMentionPrefix(this._client.CurrentUser, ref argPos))
            {
                IResult result = null;
                try
                {
                    var context = new SocketCommandContext(this._client, message);
                    result = await _commands.ExecuteAsync(context, argPos, this._services, MultiMatchHandling.Exception);
                }
                finally
                {
                    if (!result?.IsSuccess ?? false)
                    {
                        this._logger.LogError("Error Type: {0}{1}Error Reason: {2}", result.Error, Environment.NewLine, result.ErrorReason);
                    }
                }
            }
        }
    }
}
