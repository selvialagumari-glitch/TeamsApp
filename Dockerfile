# 1. Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# copy csproj files and restore
COPY ["TeamsApp.Server/TeamsApp.Server.csproj", "TeamsApp.Server/"]
COPY ["TeamsApp.Client/TeamsApp.Client.csproj", "TeamsApp.Client/"]
COPY ["TeamsApp.Shared/TeamsApp.Shared.csproj", "TeamsApp.Shared/"]

RUN dotnet restore "TeamsApp.Server/TeamsApp.Server.csproj"

# copy everything else
COPY . .

# publish the server project
WORKDIR "/src/TeamsApp.Server"
RUN dotnet publish "TeamsApp.Server.csproj" -c Release -o /app/publish

# 2. Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TeamsApp.Server.dll"]
