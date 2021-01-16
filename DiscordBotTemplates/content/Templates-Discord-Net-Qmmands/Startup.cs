using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DiscordBotTemplates.Discord.Net.Services;
using System.Threading.Tasks;

namespace DiscordBotTemplates.Discord.Net
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
                    config.AddEnvironmentVariables(prefix: "DiscordBotTemplates.Discord.Net_")
                        // .AddJsonFile("config.json")
                        .AddCommandLine(args);
#if DEBUG
                        // Right click on your project and select "Manage User Secrets" to use this provider.
                        // Otherwise, you can safely remove it.
                        config.AddUserSecrets<Startup>(optional: true);
#endif
                })
                .ConfigureDiscord(options => 
                {
                    // Configure your DiscordSocketClient here, or keep the default settings.
                })
                .ConfigureCommands(options => 
                {
                    // Configure your CommandService here, or keep the default settings.
                })
                .ConfigureServices((context, services) => 
                {
                    // Add services to your IOC container.
                    services.AddHostedService<DiscordBotService>();

                    // Event handler supplied as an example.
                    services.AddHostedService<SampleEventHandler>();
                })
                .ConfigureLogging((context, logging) => 
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.SetMinimumLevel(context.HostingEnvironment.EnvironmentName == Environments.Development
                        ? LogLevel.Debug
                        : LogLevel.Information);
                })
                .RunConsoleAsync();

            return hostingTask;
        }
    }
}
