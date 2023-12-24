FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /
COPY "src/World.Api" "src/World.Api/"
WORKDIR src/World.Api
RUN dotnet publish World.Api.csproj -c Release -o /app

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "World.Api.dll"]
