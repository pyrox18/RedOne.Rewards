using RedOne.Rewards.Application.Dtos;
using RedOne.Rewards.Application.Interfaces;
using RedOne.Rewards.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedOne.Rewards.Application.Services
{
    public class RewardService : IRewardService
    {
        private readonly IRewardRepository _rewardRepository;

        public RewardService(IRewardRepository rewardRepository)
        {
            _rewardRepository = rewardRepository;
        }

        public async Task<IEnumerable<RewardDto>> GetRewardsAsync()
        {
            var rewards = await _rewardRepository.GetRewardsAsync();

            return rewards.Select(r => new RewardDto(r));
        }
    }
}
