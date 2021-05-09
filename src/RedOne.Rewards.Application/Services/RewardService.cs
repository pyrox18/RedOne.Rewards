using RedOne.Rewards.Application.Dtos;
using RedOne.Rewards.Application.Exceptions;
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
        private readonly IMemberLevelRepository _memberLevelRepository;

        public RewardService(
            IRewardRepository rewardRepository,
            IMemberLevelRepository memberLevelRepository)
        {
            _rewardRepository = rewardRepository;
            _memberLevelRepository = memberLevelRepository;
        }

        public async Task<IEnumerable<RewardDto>> GetRewardsAsync()
        {
            var rewards = await _rewardRepository.GetRewardsAsync();

            return rewards.Select(r => new RewardDto(r));
        }

        public async Task<RewardDto> CreateRewardAsync(CreateRewardDto dto)
        {
            // Get minimum member level record for ID
            // Validation of member level existence already handled by DTO validator
            var memberLevel = await _memberLevelRepository.GetMemberLevelByLevelAsync(dto.MinimumMemberLevel);

            // Insert reward record
            var rewardEntity = dto.ToReward();
            rewardEntity.MinimumMemberLevelId = memberLevel.Id;
            var resultEntity = await _rewardRepository.InsertAsync(rewardEntity);

            return new RewardDto(resultEntity);
        }
    }
}
