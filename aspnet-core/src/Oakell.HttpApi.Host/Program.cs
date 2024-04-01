using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
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

            // builder.Services.Configure<ForwardedHeadersOptions>(options =>
            // {
            //     options.ForwardedHeaders = 
            //         ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            //     // If behind a load balancer, such as AWS ELB, you may need to add its IP address here
            //     options.KnownProxies.Add(IPAddress.Parse("a7c5b599338a443bc8666bb06451da99-2125813449.af-south-1.elb.amazonaws.com"));
            // });

            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();
            await builder.AddApplicationAsync<OakellHttpApiHostModule>();
            var app = builder.Build();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            await app.InitializeApplicationAsync();
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
