# Use the official Microsoft .NET SDK image to build the project files
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Install the ABP CLI tool
RUN dotnet tool install -g Volo.Abp.Cli
ENV PATH="$PATH:/root/.dotnet/tools"

# Install Node.js
RUN apt-get update && \
    apt-get install -y curl && \
    curl -sL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs

# Install Yarn
RUN npm install -g yarn

# Copy csproj and restore as distinct layers
COPY ["src/Oakell.HttpApi.Host/Oakell.HttpApi.Host.csproj", "Oakell.HttpApi.Host/"]
COPY ["src/Oakell.DbMigrator/Oakell.DbMigrator.csproj", "Oakell.DbMigrator/"]
RUN dotnet restore "Oakell.HttpApi.Host/Oakell.HttpApi.Host.csproj"
RUN dotnet restore "Oakell.DbMigrator/Oakell.DbMigrator.csproj"

# Copy everything else and install ABP libraries with Yarn
COPY ./ ./
RUN abp install-libs --npm-client yarn
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

# Create directories for HttpApi.Host and DbMigrator
WORKDIR /app
RUN mkdir -p /app/Oakell.HttpApi.Host /app/Oakell.DbMigrator

# Copy published files
COPY --from=publish-api /app/publish /app/Oakell.HttpApi.Host
COPY --from=publish-db-migrator /app/publish-db-migrator /app/Oakell.DbMigrator

# Copy the entire project directories to maintain the folder structure
COPY src/Oakell.HttpApi.Host/ /app/Oakell.HttpApi.Host/
COPY src/Oakell.DbMigrator/ /app/Oakell.DbMigrator/

# Copy the startup script
COPY ./scripts/startup.sh /app/startup.sh

RUN chmod +x /app/startup.sh
ENTRYPOINT ["/app/startup.sh"]
