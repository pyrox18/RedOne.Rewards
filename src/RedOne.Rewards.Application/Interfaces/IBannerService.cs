using RedOne.Rewards.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedOne.Rewards.Application.Interfaces
{
    public interface IBannerService
    {
        Task<IEnumerable<BannerDto>> GetBannersAsync();
    }
}
