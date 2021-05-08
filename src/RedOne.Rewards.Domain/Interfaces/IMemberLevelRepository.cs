using RedOne.Rewards.Domain.Entities;
using System.Threading.Tasks;

namespace RedOne.Rewards.Domain.Interfaces
{
    public interface IMemberLevelRepository
    {
        Task InsertAsync(MemberLevel memberLevel);
        Task<bool> IsEmptyAsync();
    }
}
