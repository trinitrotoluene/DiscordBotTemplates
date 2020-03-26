using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DiscordBotTemplates.DSharpPlus.Services;
using DSharpPlus;

using msft = Microsoft.Extensions.Logging;

namespace DiscordBotTemplates.DSharpPlus
{
    internal sealed class Startup
    {
        static Task Main(string[] args)
        {
            var hostingTask = new HostBuilder()
#if DEBUG
                .UseEnvironment(Environments.Development)
#else
                .UseEnvironment(Environments.Production)
#endif
                .ConfigureAppConfiguration(config => 
                {
                    config.AddEnvironmentVariables(prefix: "DiscordBotTemplates.DSharpPlus_")
                        // .AddJsonFile("config.json")
                        .AddCommandLine(args);
#if DEBUG
                        // Right click on your project and select "Manage User Secrets" to use this provider.
                        // Otherwise, you can safely remove it.
                        config.AddUserSecrets<Startup>(optional: true);
#endif
                })
                .ConfigureDiscord((context, options) => 
                {
                    // Configure your DiscordClient here.
                    options.TokenType = TokenType.Bot;
                    options.Token = context.Configuration["token"];

                    options.UseInternalLogHandler = false;
                })
                .ConfigureCNext((ctx, options) =>
                {
                    options.StringPrefix = ctx.Configuration["prefix"];
                })
                .ConfigureServices((context, services) => 
                {
                    // This service handles registering & configuring the CommandsNextModule for your DiscordClient.
                    services.AddHostedService<CommandsNextRegistrationService>();

                    // Add services to your IOC container.
                    services.AddHostedService<DiscordBotService>();
                })
                .ConfigureLogging((context, logging) => 
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.SetMinimumLevel(context.HostingEnvironment.EnvironmentName == Environments.Development
                        ? msft::LogLevel.Debug
                        : msft::LogLevel.Information);
                })
                .RunConsoleAsync();

            return hostingTask;
        }
    }
}
