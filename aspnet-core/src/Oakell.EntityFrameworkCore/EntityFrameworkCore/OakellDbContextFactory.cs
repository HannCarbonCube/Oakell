using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Oakell.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class OakellDbContextFactory : IDesignTimeDbContextFactory<OakellDbContext>
    {
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

        private static IConfigurationRoot BuildConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Oakell.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
