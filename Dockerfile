# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy solution and project files
COPY *.sln . 
COPY TeamsApp.Server/TeamsApp.Server.csproj ./TeamsApp.Server/
COPY TeamsApp.Client/TeamsApp.Client.csproj ./TeamsApp.Client/
COPY TeamsApp.Shared/TeamsApp.Shared.csproj ./TeamsApp.Shared/

# Restore dependencies
RUN dotnet restore

# Copy everything else
COPY . .

# Build and publish the server project (includes client automatically)
RUN dotnet publish TeamsApp.Server/TeamsApp.Server.csproj -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/publish .

# Expose Render port
ENV ASPNETCORE_URLS=http://+:$PORT
EXPOSE $PORT

# Start the server app
ENTRYPOINT ["dotnet", "TeamsApp.Server.dll"]
