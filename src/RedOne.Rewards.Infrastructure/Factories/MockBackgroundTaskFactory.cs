using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RedOne.Rewards.Infrastructure.Factories
{
    public class MockBackgroundTaskFactory : IMockBackgroundTaskFactory
    {
        private readonly ILogger<MockBackgroundTaskFactory> _logger;

        public MockBackgroundTaskFactory(ILogger<MockBackgroundTaskFactory> logger)
        {
            _logger = logger;
        }

        public Func<CancellationToken, Task> CreateMockRewardRedemptionEmailTask(
            string emailAddress,
            string rewardTitle,
            int pointsRedeemed)
        {
            return async cancellationToken =>
            {
                _logger.LogInformation($"Sending mock email to {emailAddress} on redemption of {rewardTitle} ({pointsRedeemed} points)... (2 sec delay)");
                await Task.Delay(2000);
                _logger.LogInformation("Mock email sent.");
            };
        }
    }
}
