using DSharpPlus;
using DSharpPlus.CommandsNext;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordBotTemplates.DSharpPlus.Services
{
    public class CommandsNextRegistrationService : IHostedService
    {
        private readonly ILogger<CommandsNextRegistrationService> _logger;
        private readonly DiscordClient _client;
        private readonly CommandsNextConfiguration _cnextConfig;

        private CommandsNextModule _cnext;

        public CommandsNextRegistrationService(ILogger<CommandsNextRegistrationService> logger, DiscordClient client, CommandsNextConfiguration cnextConfig = null)
        {
            this._logger = logger;
            this._client = client;
            this._cnextConfig = cnextConfig;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (_cnextConfig == null)
            {
                this._logger.LogWarning("No CommandsNextConfiguration was found. If you aren't using CNext, unregister this service!");
                return Task.CompletedTask;
            }

            this._cnext = this._client.UseCommandsNext(this._cnextConfig);

            this._cnext.RegisterCommands(Assembly.GetEntryAssembly());

            this._cnext.CommandExecuted += LogExecutedCommand;
            this._cnext.CommandErrored += LogErroredCommand;

            this._logger.LogInformation("Registered {0} commands successfully", this._cnext.RegisteredCommands.Count);
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private Task LogErroredCommand(CommandErrorEventArgs e)
        {
            this._logger.LogError("An exception was thrown while executing command '{0}' for user '{1}'.\r\n{2}",
                e.Command.Name,
                e.Context.User.ToString(),
                e.Exception.StackTrace);

            return Task.CompletedTask;
        }

        private Task LogExecutedCommand(CommandExecutionEventArgs e)
        {
            this._logger.LogInformation("Executed command '{0}' for user '{1}' in guild '{2}'", 
                e.Command.Name, 
                e.Context.User.ToString(),
                e.Context.Guild.Name ?? "N/A (Direct Message)");

            return Task.CompletedTask;
        }
    }
}
