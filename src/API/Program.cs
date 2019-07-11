using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
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
                .MinimumLevel.Debug()
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
                    .Build();

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal("Application has been closed unexpectedly");
                return 1;
            }
            finally
            {
                Log.Information("Application is exiting...");
                Log.CloseAndFlush();
            }

            return 0;
        }

        private static Action<KestrelServerOptions> KestrelOptions()
        {
            return o =>
            {
                o.Listen(
                    IPAddress.Parse("127.0.0.1"),
                    5000,
                    options => { options.UseHttps(new X509Certificate2(@"./certs/tls.pfx", "somepassword")); });
            };
        }
    }
}
