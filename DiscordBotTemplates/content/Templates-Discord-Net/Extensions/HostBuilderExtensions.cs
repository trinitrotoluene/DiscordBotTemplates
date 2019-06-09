using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureDiscord(this IHostBuilder builder, Action<DiscordSocketConfig> configureOptions)
        {
            var config = new DiscordSocketConfig();
            configureOptions(config);

            var client = new DiscordSocketClient(config);

            builder.ConfigureServices(services =>
            {
                services.AddSingleton(config);
                services.AddSingleton(client);
            });

            return builder;
        }
    }
}
