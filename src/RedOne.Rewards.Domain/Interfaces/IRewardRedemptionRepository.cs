using System.Threading.Tasks;

namespace RedOne.Rewards.Domain.Interfaces
{
    public interface IRewardRedemptionRepository
    {
        Task<int> RedeemRewardAsync(string userPhoneNumber, int rewardId);
    }
}
