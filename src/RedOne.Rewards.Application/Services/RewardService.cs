﻿using RedOne.Rewards.Application.Dtos;
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

        public async Task<IEnumerable<RewardDto>> GetRewardsAsync(bool sortByMemberLevel = false)
        {
            var rewards = await _rewardRepository.GetRewardsAsync(sortByMemberLevel);

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

        public async Task DeleteRewardAsync(int id)
        {
            if (!await _rewardRepository.ExistsAsync(id))
                throw new NotFoundException($"Reward with ID {id} not found.");

            await _rewardRepository.DeleteByIdAsync(id);
        }

        public async Task<ConsumerUserRewardInfoDto> GetConsumerUserRewardInfoAsync(string phoneNumber)
        {
            var result = await _rewardRepository.GetConsumerUserRewardInfoAsync(phoneNumber);

            return new ConsumerUserRewardInfoDto(result);
        }
    }
}
