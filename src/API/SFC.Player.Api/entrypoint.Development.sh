#!/bin/sh

./src/API/SFC.Player.Api/entrypoint.Common.sh
dotnet run --project /app/src/API/SFC.Player.Api/SFC.Player.Api.csproj --no-launch-profile