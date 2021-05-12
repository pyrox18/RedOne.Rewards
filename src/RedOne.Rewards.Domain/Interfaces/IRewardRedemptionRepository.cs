using RedOne.Rewards.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RedOne.Rewards.Domain.Interfaces
{
    public interface IRewardRedemptionRepository
    {
        Task<int> RedeemRewardAsync(string userPhoneNumber, int rewardId);
        Task<IEnumerable<RewardRedemption>> GetRewardRedemptionsAsync(string userPhoneNumber);
    }
}
