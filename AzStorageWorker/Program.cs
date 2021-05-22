using Azure.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AzStorageWorkerDemo
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
                    services.AddHostedService<AzStorageWorker>();                  
                    services.AddSingleton<ChainedTokenCredential>(GetChainedTokenCredential());
                });

        private static ChainedTokenCredential GetChainedTokenCredential() => new ChainedTokenCredential(
            new  AzureCliCredential(),
            new ManagedIdentityCredential()
        );
    }
}
