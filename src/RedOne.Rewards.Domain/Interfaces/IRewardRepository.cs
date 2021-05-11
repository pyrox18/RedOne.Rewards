using RedOne.Rewards.Domain.Entities;
using RedOne.Rewards.Domain.Models;
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
        Task<ConsumerUserRewardInfoSpModel> GetConsumerUserRewardInfoAsync(string phoneNumber);
        Task<Reward> GetByIdAsync(int id);
    }
}
