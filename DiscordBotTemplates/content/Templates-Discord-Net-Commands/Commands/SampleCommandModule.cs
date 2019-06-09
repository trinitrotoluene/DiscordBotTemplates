using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordBotTemplates.Discord.Net.Commands
{
    public class SampleCommandModule : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public Task PongAsync()
        {
            return base.ReplyAsync("Pong!");
        }
    }
}
