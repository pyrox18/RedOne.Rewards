using System;
using System.Threading;
using System.Threading.Tasks;

namespace RedOne.Rewards.Infrastructure.Factories
{
    public interface IMockBackgroundTaskFactory
    {
        Func<CancellationToken, Task> CreateMockRewardRedemptionEmailTask(
            string emailAddress,
            string rewardTitle,
            int pointsRedeemed);
    }
}
