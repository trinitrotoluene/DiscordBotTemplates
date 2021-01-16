using Discord;
using Qmmands;
using System.Threading.Tasks;

namespace DiscordBotTemplates.Discord.Net.Commands
{
    public abstract class DiscordModuleBase : ModuleBase<DiscordCommandContext>
    {
        protected Task ReplyAsync(string message = null, bool isTts = false, Embed embed = null, RequestOptions requestOptions = null)
        {
            return Context.Channel.SendMessageAsync(message, isTts, embed, requestOptions);
        }
    }
}
