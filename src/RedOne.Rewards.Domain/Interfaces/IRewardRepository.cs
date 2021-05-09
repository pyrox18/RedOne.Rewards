using RedOne.Rewards.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedOne.Rewards.Domain.Interfaces
{
    public interface IRewardRepository
    {
        Task<IEnumerable<Reward>> GetRewardsAsync(bool sortByMemberLevel = false);
        Task<Reward> InsertAsync(Reward reward);
        Task<bool> ExistsAsync(int id);
        Task DeleteByIdAsync(int id);
    }
}
