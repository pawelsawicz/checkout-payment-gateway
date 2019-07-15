using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace API
{
    public class Program
    {
        private readonly string ApplicationName = "payment-gateway-api";

        private readonly string ApplicationVersion = "0.0.1";
        
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .WriteTo.Console(
                    outputTemplate:
                    "{NewLine}{Timestamp:HH:mm:ss} [{Level}] ({CorrelationToken}) {Message}{NewLine}{Exception}")
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
                        x.AddEnvironmentVariables();
                        x.AddJsonFile("appsettings.json");
                    })
                    .Build();

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal("Application has been closed unexpectedly");
                Log.Fatal(ex.ToString());
                return 1;
            }
            finally
            {
                Log.Information("Application is exiting...");
                Log.CloseAndFlush();
            }

            return 0;
        }
    }
}
