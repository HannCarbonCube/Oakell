# Use the official Microsoft .NET SDK image to build the project files
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Install the ABP CLI tool
RUN dotnet tool install -g Volo.Abp.Cli
ENV PATH="$PATH:/root/.dotnet/tools"

# Copy csproj and restore as distinct layers
COPY ["src/Oakell.HttpApi.Host/Oakell.HttpApi.Host.csproj", "Oakell.HttpApi.Host/"]
COPY ["src/Oakell.DbMigrator/Oakell.DbMigrator.csproj", "Oakell.DbMigrator/"]
RUN dotnet restore "Oakell.HttpApi.Host/Oakell.HttpApi.Host.csproj"
RUN dotnet restore "Oakell.DbMigrator/Oakell.DbMigrator.csproj"
 
# Copy everything else and build and install ABP libraries
COPY ./ ./
RUN abp install-libs
RUN dotnet build "src/Oakell.HttpApi.Host/Oakell.HttpApi.Host.csproj" -c Release -o /app/build
RUN dotnet build "src/Oakell.DbMigrator/Oakell.DbMigrator.csproj" -c Release -o /app/build-db-migrator

# Publish HttpApi.Host
FROM build AS publish-api
RUN dotnet publish "src/Oakell.HttpApi.Host/Oakell.HttpApi.Host.csproj" -c Release -o /app/publish

# Publish DbMigrator
FROM build AS publish-db-migrator
RUN dotnet publish "src/Oakell.DbMigrator/Oakell.DbMigrator.csproj" -c Release -o /app/publish-db-migrator


# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish-api /app/publish .
COPY --from=publish-db-migrator /app/publish-db-migrator ./dbmigrator
COPY ./scripts/startup.sh ./startup.sh
RUN chmod +x ./startup.sh
ENTRYPOINT ["./startup.sh"]
