using Discord;
using Qmmands;
using System.Threading.Tasks;

namespace DiscordBotTemplates.Discord.Net.Commands
{
    public abstract class DiscordModuleBase : ModuleBase<CommandContext>
    {
        protected Task ReplyAsync(string message = null, bool isTTS = false, Embed embed = null, RequestOptions requestOptions = null)
        {
            return this.Context.Channel.SendMessageAsync(message, isTTS, embed, requestOptions);
        }
    }
}
