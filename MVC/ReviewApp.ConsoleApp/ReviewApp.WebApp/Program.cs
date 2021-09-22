using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Serilog;
using Microsoft.Extensions.Logging.Configuration;
using System.IO;

namespace ReviewApp.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
                .AddCommandLine(args)
                .Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .CreateLogger();

            
            try
            {
                Log.Information("Application Starting Up");
                CreateHostBuilder(args).Build().Run();
                Log.Information("Shutting down...");
            }
            catch(Exception ex)
            {
                Log.Fatal(ex, "The application failed to start.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()


             .ConfigureAppConfiguration((context, config) =>
             {
             var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
             config.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
             })
             .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder.UseStartup<Startup>();
             });
    }
}
