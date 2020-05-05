using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using System;
using Microsoft.Extensions.Configuration;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.Async(p => p.File("log-.txt", rollOnFileSizeLimit: true, fileSizeLimitBytes: 10000000, rollingInterval: RollingInterval.Month, restrictedToMinimumLevel: LogEventLevel.Information))
                .CreateLogger();
            try
            {
                Log.Information("Application started.");
                CreateWebHostBuilder(args)
                    .Build().Run();

            }
            catch (Exception e)
            {
                Log.Fatal(e, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseKestrel()
                .UseStartup<Startup>().ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                });
    }
}
