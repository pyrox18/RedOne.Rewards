using RedOne.Rewards.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedOne.Rewards.Application.Interfaces
{
    public interface IMemberLevelService
    {
        Task SeedMemberLevelDataAsync();
        Task<IEnumerable<MemberLevelDto>> GetMemberLevelsAsync();
    }
}
