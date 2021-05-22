using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AzStorageWorkerDemo
{
    public class AzStorageWorker : BackgroundService
    {
        private readonly ILogger<AzStorageWorker> _logger;
        private readonly ChainedTokenCredential _tokenCredential;

        private string accessToken;
        public AzStorageWorker(ILogger<AzStorageWorker> logger, ChainedTokenCredential tokenCredential)
        {
            _logger = logger;
            _tokenCredential = tokenCredential;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var blobUri = "https://cmdemo123.blob.core.windows.net/";
            var blobClient = new BlobServiceClient(new Uri(blobUri), _tokenCredential);
            var container = blobClient.GetBlobContainerClient("sample-data");
            
            while (!stoppingToken.IsCancellationRequested)
            {
                await foreach(var blob in container.GetBlobsAsync())
                {
                    _logger.LogInformation($"The blob name is: {blob.Name}");
                }

                _logger.LogInformation("Blob enumeration completed!");
                _logger.LogInformation(string.Empty);
            }
        }
    }
}
