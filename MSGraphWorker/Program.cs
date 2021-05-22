using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client;

namespace MSGraphWorkerDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<MSGraphWorker>();
                    services.AddSingleton<IConfidentialClientApplication>(GetConfidentialClient());
                });

        private static IConfidentialClientApplication GetConfidentialClient()
        {
            AuthenticationConfig config = AuthenticationConfig
                                            .ReadFromJsonFile("appsettings.json");
            IConfidentialClientApplication app;
                app = ConfidentialClientApplicationBuilder
                        .Create(config.ClientId)
                        .WithClientSecret(config.ClientSecret)
                        .WithAuthority(new Uri(config.Authority))
                        .Build();

            return app;
        }
    }
}
