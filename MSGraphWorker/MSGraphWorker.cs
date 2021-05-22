using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;

namespace MSGraphWorkerDemo
{
    public class MSGraphWorker : BackgroundService
    {
        private readonly ILogger<MSGraphWorker> _logger;
        private readonly IConfidentialClientApplication _app;
        public MSGraphWorker(ILogger<MSGraphWorker> logger, IConfidentialClientApplication app)
        {
            _logger = logger;
            _app = app;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var graphClient = new GraphServiceClient(new ClientCredentialProvider(_app));
            while (!stoppingToken.IsCancellationRequested)
            {
                var directoryUsers = await graphClient.Users.Request().GetAsync();
                foreach(var user in directoryUsers)
                {
                    _logger.LogInformation($"Current user is {user.DisplayName}");
                }
                _logger.LogInformation("The MS Graph query executed successfully");
                
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
