using RedOne.Rewards.Application.Dtos;
using System.Threading.Tasks;

namespace RedOne.Rewards.Application.Interfaces
{
    public interface IAdminUserService
    {
        Task<bool> AuthenticateUserAsync(AuthenticateAdminUserDto dto);
        Task<AdminUserDataDto> GetByUsernameAsync(string username);
        Task SeedAdminUserDataAsync();
    }
}
