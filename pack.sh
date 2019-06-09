#!/bin/bash

dotnet pack DiscordBotTemplates/DiscordBotTemplates.csproj -p:TemplatesPath=./bin -p:VersionPrefix=$1
