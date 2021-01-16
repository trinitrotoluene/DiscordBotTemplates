![NuGet Deploy (Release)](https://github.com/trinitrotoluene/DiscordBotTemplates/workflows/NuGet%20Deploy%20(Release)/badge.svg)
![NuGet Deploy (Prerelease)](https://github.com/trinitrotoluene/DiscordBotTemplates/workflows/NuGet%20Deploy%20(Prerelease)/badge.svg)
![Nuget](https://img.shields.io/nuget/v/DiscordBotTemplates.svg?label=DiscordBotTemplates&style=popout)

## Library Links

This project exists solely to take some of the boilerplate and hassle out of creating your initial Discord bot setup. For questions regarding how to do X or what to do if Y happens, please refer to the documentation and avenues of support provided by the libraries themselves!

- [Discord.Net](https://github.com/discord-net/Discord.Net)
- [DSharpPlus](https://github.com/DSharpPlus/DSharpPlus)

## Discord Bot Templates

ASP.NET's [Generic Host](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-2.2) decouples the HTTP pipeline from the web host API, making it available for use in many other contexts. These templates set up your discord bot as a hosted service, allowing you to easily access quality of life features like logging and configuration extensions, as well as dependency injection. 

## Currently Included Project ShortNames

```
- dnet             (Discord.Net, Minimal)
- dnet-commands    (Discord.Net, Discord.Commands)
- dnet-qmmands     (Discord.Net, Qmmands)
- dsharpplus       (DSharpPlus, Minimal)
- dsharpplus-cnext (DSharpPlus, CommandsNext)
```

The plan is to include a template for all DAPI-approved or mainstream .NET Discord libraries. If you'd like to contribute to a template or would like to request that a template be added for your library, please open an issue or make a pull request as appropriate!

## Getting Started

1. Install your chosen template using `dotnet new`
```
dotnet new --install DiscordBotTemplates
```
2. Navigate to a directory you'd like to create the project in. The (Discord.Net, Minimal) template is used in this example. Swap it out with a different shortname as required.
```
mkdir MyNewProject
dotnet new dnet
```
3. Verify that the template is working. Your bot should come online normally.
```
dotnet run --token=<your_discord_bot_token>
```

Note that some templates will require more than just the `token` configuration variable to be set. These requirements will be flagged by preprocessor warnings.

4. Open the template in your IDE or text editor of choice, and start working on your new project! Please ensure you read the defined warnings carefully before you start.

## Updating

To update an installed template, just run the installation as normal. `dotnet new` will overwrite the existing templates with the updated versions for you.

## Removing

If you'd like to remove the installed templates (note that you don't need to do this if you're updating!), simply run this command to remove the package.
```
dotnet run --uninstall DiscordBotTemplates
```
