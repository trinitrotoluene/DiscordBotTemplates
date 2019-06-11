﻿using DSharpPlus;
using DSharpPlus.CommandsNext;
using Microsoft.Extensions.Hosting;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureDiscord(this IHostBuilder builder, Action<DiscordConfiguration> configureOptions)
        {
            var config = new DiscordConfiguration();
            configureOptions(config);

            var client = new DiscordClient(config);

            builder.ConfigureServices(services =>
            {
                services.AddSingleton(config);
                services.AddSingleton(client);
            });

            return builder;
        }

        public static IHostBuilder ConfigureDiscord(this IHostBuilder builder, Action<HostBuilderContext, DiscordConfiguration> configureOptions)
        {
            builder.ConfigureServices((context, services) =>
            {
                var config = new DiscordConfiguration();
                configureOptions(context, config);

                var client = new DiscordClient(config);

                services.AddSingleton(config);
                services.AddSingleton(client);
            });

            return builder;
        }

        public static IHostBuilder ConfigureCNext(this IHostBuilder builder, Action<CommandsNextConfiguration> configureOptions)
        {
            var cnextConfig = new CommandsNextConfiguration();
            configureOptions(cnextConfig);

            builder.ConfigureServices(services =>
            {
                services.AddSingleton(cnextConfig);
            });

            return builder;
        }

        public static IHostBuilder ConfigureCNext(this IHostBuilder builder, Action<HostBuilderContext, CommandsNextConfiguration> configureOptions)
        {
            builder.ConfigureServices((ctx, services) =>
            {
                var cnextConfig = new CommandsNextConfiguration();
                configureOptions(ctx, cnextConfig);

                services.AddSingleton(cnextConfig);
            });

            return builder;
        }
    }
}
