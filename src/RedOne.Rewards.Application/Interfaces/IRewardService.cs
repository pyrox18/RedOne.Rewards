using RedOne.Rewards.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedOne.Rewards.Application.Interfaces
{
    public interface IRewardService
    {
        Task<IEnumerable<RewardDto>> GetRewardsAsync();
        Task<RewardDto> CreateRewardAsync(CreateRewardDto dto);
    }
}
