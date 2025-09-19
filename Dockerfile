# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# copy everything
COPY . .

# restore at solution level so all projects are pulled in
RUN dotnet restore TeamsApp.sln

# publish the Server project (this will also build Client & Shared)
RUN dotnet publish TeamsApp.Server/TeamsApp.Server.csproj -c Release -o /app

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app .

# Render uses $PORT (default 10000)
ENV ASPNETCORE_URLS=http://+:${PORT:-10000}
EXPOSE 10000

ENTRYPOINT ["dotnet", "TeamsApp.Server.dll"]
