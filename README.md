![AppVeyor](https://img.shields.io/appveyor/ci/trinitrot0luene/DiscordBotTemplates.svg?style=popout)
![Nuget](https://img.shields.io/nuget/v/DiscordBotTemplates.svg?label=DiscordBotTemplates&style=popout)

## Discord Bot Templates

ASP.NET's [Generic Host](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-2.2) decouples the HTTP pipeline from the web host API, making it available for use in many other contexts. These templates set up your discord bot as a hosted service, allowing you to easily access quality of life features like logging and configuration extensions, as well as dependency injection. 

## Currently Included Project ShortNames

```
- dnet (Discord.Net)
```

## Getting Started

1. Install your chosen template using `dotnet new`
```
dotnet new -i DiscordBotTemplates
```
2. Navigate to a directory you'd like to create the project in.
```
mkdir MyNewProject
dotnet new dnet
```
3. Verify that the template is working. Your bot should come online normally.
```
dotnet run --token=<your_discord_bot_token>
```
4. Open the template in your IDE or text editor of choice, and start working on your new project! Please ensure you read the defined warnings carefully before you start.

### Removing
```
dotnet run --uninstall DiscordBotTemplates
```
