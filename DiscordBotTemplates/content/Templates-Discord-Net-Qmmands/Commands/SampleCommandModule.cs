using Qmmands;
using System.Threading.Tasks;

namespace DiscordBotTemplates.Discord.Net.Commands
{
    public class SampleCommandModule : DiscordModuleBase
    {
        [Command("ping")]
        public Task PongAsync()
        {
            return base.ReplyAsync("Pong!");
        }
    }
}
