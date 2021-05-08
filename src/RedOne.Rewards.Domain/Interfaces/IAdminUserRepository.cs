using RedOne.Rewards.Domain.Entities;
using System.Threading.Tasks;

namespace RedOne.Rewards.Domain.Interfaces
{
    public interface IAdminUserRepository
    {
        Task<AdminUser> GetAdminUserByUsernameAsync(string username);
        Task InsertAsync(string username, string hashedPassword);
    }
}
