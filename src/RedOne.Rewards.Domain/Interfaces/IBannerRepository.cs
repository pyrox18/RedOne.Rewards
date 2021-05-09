using RedOne.Rewards.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedOne.Rewards.Domain.Interfaces
{
    public interface IBannerRepository
    {
        Task<IEnumerable<Banner>> GetAllAsync();
        Task<Banner> InsertAsync(Banner banner);
        Task<bool> ExistsAsync(int id);
        Task DeleteByIdAsync(int id);
    }
}
