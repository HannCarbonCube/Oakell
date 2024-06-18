using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Oakell;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

        try
        {
            Log.Information("Starting Oakell.HttpApi.Host.");
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();

            // Retrieve the configuration
            var configuration = builder.Configuration;

            // Log the Client Id from the configuration
            var clientId = configuration["AuthServer:ClientId"];
            

            await builder.AddApplicationAsync<OakellHttpApiHostModule>();
            var app = builder.Build();
            await app.InitializeApplicationAsync();

            app.Use(async (context, next) =>
            {
                Log.Information("Request Scheme: {Scheme}", context.Request.Scheme);
                Log.Information("X-Forwarded-Proto: {XForwardedProto}", context.Request.Headers["X-Forwarded-Proto"]);
                await next.Invoke();
            });

            Log.Information("Client Id: {ClientId}", clientId);

            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            if (ex is HostAbortedException)
            {
                throw;
            }

            Log.Fatal(ex, "Host terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}