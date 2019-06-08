using Discord.Commands;
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

        public static IHostBuilder ConfigureCommands(this IHostBuilder builder, Action<CommandServiceConfig> configureOptions)
        {
            var config = new CommandServiceConfig();
            configureOptions(config);

            var commands = new CommandService(config);

            builder.ConfigureServices(services =>
            {
                services.AddSingleton(config);
                services.AddSingleton(commands);
            });

            return builder;
        }
    }
}
