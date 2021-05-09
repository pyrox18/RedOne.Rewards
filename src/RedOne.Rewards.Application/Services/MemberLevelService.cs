using RedOne.Rewards.Application.Dtos;
using RedOne.Rewards.Application.Interfaces;
using RedOne.Rewards.Domain.Entities;
using RedOne.Rewards.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedOne.Rewards.Application.Services
{
    public class MemberLevelService : IMemberLevelService
    {
        private readonly IMemberLevelRepository _memberLevelRepository;

        public MemberLevelService(IMemberLevelRepository memberLevelRepository)
        {
            _memberLevelRepository = memberLevelRepository;
        }

        public async Task<IEnumerable<MemberLevelDto>> GetMemberLevelsAsync()
        {
            var result = await _memberLevelRepository.GetMemberLevelsAsync();

            return result.Select(r => new MemberLevelDto(r));
        }

        public async Task SeedMemberLevelDataAsync()
        {
            if (await _memberLevelRepository.IsEmptyAsync())
            {
                var memberLevels = new List<MemberLevel>
                {
                    new MemberLevel
                    {
                        Level = 1,
                        LevelText = "Platinum",
                        Threshold = 1200
                    },
                    new MemberLevel
                    {
                        Level = 2,
                        LevelText = "Gold",
                        Threshold = 1000
                    },
                    new MemberLevel
                    {
                        Level = 3,
                        LevelText = "Silver",
                        Threshold = 500
                    },
                    new MemberLevel
                    {
                        Level = 4,
                        LevelText = "Bronze",
                        Threshold = 0
                    }
                };

                foreach (var level in memberLevels)
                {
                    await _memberLevelRepository.InsertAsync(level);
                }
            }
        }
    }
}
