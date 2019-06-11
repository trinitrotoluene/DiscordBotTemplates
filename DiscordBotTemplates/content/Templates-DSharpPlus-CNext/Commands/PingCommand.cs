using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace DiscordBotTemplates.DSharpPlus.Commands
{
    public class PingCommand
    {
        [Command("ping")]
        public Task PingAsync(CommandContext ctx)
        {
            return ctx.RespondAsync("Pong!");
        }
    }
}
