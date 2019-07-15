using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .WriteTo.Console(
                    outputTemplate:
                    "{NewLine}{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
                .CreateLogger();

            try
            {
                var host = new WebHostBuilder()
                    .UseKestrel()
                    .UseStartup<Startup>()
                    .UseIISIntegration()
                    .UseSerilog()
                    .ConfigureAppConfiguration(x=>
                    {
                        x.AddJsonFile("appsettings.json");
                    })
                    .Build();

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal("Application has been closed unexpectedly - {Message}", ex.Message);
            }
            finally
            {
                Log.Information("Application is exiting...");
                Log.CloseAndFlush();
            }
        }
    }
}
