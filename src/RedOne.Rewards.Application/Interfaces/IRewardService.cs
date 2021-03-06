using RedOne.Rewards.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedOne.Rewards.Application.Interfaces
{
    public interface IRewardService
    {
        Task<IEnumerable<RewardDto>> GetRewardsAsync(bool sortByMemberLevel = false);
        Task<RewardDto> CreateRewardAsync(CreateRewardDto dto);
        Task DeleteRewardAsync(int id);
        Task<ConsumerUserRewardInfoDto> GetConsumerUserRewardInfoAsync(string phoneNumber);
        Task RedeemRewardAsync(string userPhoneNumber, int rewardId);
        Task<IEnumerable<RewardRedemptionDto>> GetConsumerUserRewardRedemptionsAsync(string phoneNumber);
    }
}
