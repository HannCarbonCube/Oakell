using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Oakell.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class OakellDbContextFactory : IDesignTimeDbContextFactory<OakellDbContext>
    {
        public ILogger<OakellDbContextFactory> Logger { get; set; }

        public OakellDbContextFactory()
        {
            Logger = NullLogger<OakellDbContextFactory>.Instance;
        }

        public OakellDbContext CreateDbContext(string[] args)
        {
            // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            OakellEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<OakellDbContext>()
                .UseNpgsql(configuration.GetConnectionString("Default"));

            return new OakellDbContext(builder.Options);
        }

        private IConfigurationRoot BuildConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            Logger.LogInformation("Environment: {Environment}", environment);

            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Oakell.DbMigrator/"))
                .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            LogConfiguration(configuration);

            return configuration;
        }

        private void LogConfiguration(IConfiguration configuration)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            Logger.LogInformation("Current Directory: {CurrentDirectory}", currentDirectory);
            Logger.LogInformation("Environment: {Environment}", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development");

            foreach (var kvp in configuration.AsEnumerable())
            {
                Logger.LogInformation("{Key}: {Value}", kvp.Key, kvp.Value);
            }
        }
    }
}
