using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RedOne.Rewards.Infrastructure.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RedOne.Rewards.Infrastructure.Services
{
    public class BackgroundTaskHostedService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IBackgroundTaskQueue _backgroundTaskQueue;

        public BackgroundTaskHostedService(
            ILogger logger,
            IBackgroundTaskQueue backgroundTaskQueue)
        {
            _logger = logger;
            _backgroundTaskQueue = backgroundTaskQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting background task service.");

            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await _backgroundTaskQueue.DequeueAsync(stoppingToken);

                try
                {
                    await workItem(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error occurred while executing {nameof(workItem)}.");
                }
            }
        }
    }
}
