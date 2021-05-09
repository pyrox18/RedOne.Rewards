using RedOne.Rewards.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedOne.Rewards.Domain.Interfaces
{
    public interface IBannerRepository
    {
        Task<IEnumerable<Banner>> GetAllAsync();
    }
}
