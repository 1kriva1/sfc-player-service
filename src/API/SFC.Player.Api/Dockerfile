#base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
LABEL service="SFC.Player.Api" author="Kryvoruk Andrii" email="krivorukandrey@gmail.com"
USER root
WORKDIR /app

#restore & build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
ENV HUSKY=False
WORKDIR /source
COPY ["Directory.Build.props", "."]
COPY ["nuget.config", "."]
COPY ["src/API/SFC.Player.Api/SFC.Player.Api.csproj", "src/API/SFC.Player.Api/"]
COPY ["src/Core/SFC.Player.Application/SFC.Player.Application.csproj", "src/Core/SFC.Player.Application/"]
COPY ["src/Core/SFC.Player.Domain/SFC.Player.Domain.csproj", "src/Core/SFC.Player.Domain/"]
COPY ["src/Infrastructure/SFC.Player.Infrastructure/SFC.Player.Infrastructure.csproj", "src/Infrastructure/SFC.Player.Infrastructure/"]
COPY ["src/Infrastructure/SFC.Player.Infrastructure.Persistence/SFC.Player.Infrastructure.Persistence.csproj", "src/Infrastructure/SFC.Player.Infrastructure.Persistence/"]
COPY ["src/Contracts/SFC.Player.Contracts/SFC.Player.Contracts.csproj", "src/Contracts/SFC.Player.Contracts/"]
COPY ["src/Contracts/SFC.Player.Messages/SFC.Player.Messages.csproj", "src/Contracts/SFC.Player.Messages/"]
RUN dotnet restore "./src/API/SFC.Player.Api/SFC.Player.Api.csproj" --property:HUSKY=$HUSKY
COPY [".", "."]
WORKDIR "/source/src/API/SFC.Player.Api"
RUN dotnet build "./SFC.Player.Api.csproj" --configuration $BUILD_CONFIGURATION --property:HUSKY=$HUSKY --output /app/build

#publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SFC.Player.Api.csproj" --configuration $BUILD_CONFIGURATION --property:HUSKY=$HUSKY --property:UseAppHost=false --output /app/publish

#final
FROM base AS final
ARG HTTP_PORT=8080
ARG HTTPS_PORT=8081
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE $HTTP_PORT
EXPOSE $HTTPS_PORT
ENTRYPOINT ["bash", "entrypoint.sh"]