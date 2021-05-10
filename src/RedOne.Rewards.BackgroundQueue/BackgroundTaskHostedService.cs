using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RedOne.Rewards.BackgroundQueue
{
    public class BackgroundTaskHostedService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IBackgroundTaskQueue _backgroundTaskQueue;

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
