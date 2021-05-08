using CryptoHelper;
using RedOne.Rewards.Application.Dtos;
using RedOne.Rewards.Application.Interfaces;
using RedOne.Rewards.Domain.Interfaces;
using System.Threading.Tasks;

namespace RedOne.Rewards.Application.Services
{
    public class AdminUserService : IAdminUserService
    {
        private readonly IAdminUserRepository _adminUserRepository;

        public AdminUserService(IAdminUserRepository adminUserRepository)
        {
            _adminUserRepository = adminUserRepository;
        }

        public async Task<bool> AuthenticateUserAsync(AuthenticateAdminUserDto dto)
        {
            var adminUser = await _adminUserRepository.GetAdminUserByUsernameAsync(dto.Username);
            if (adminUser is null)
                return false;

            return Crypto.VerifyHashedPassword(adminUser.Password, dto.Password);
        }

        public async Task<AdminUserDataDto> GetByUsernameAsync(string username)
        {
            var adminUser = await _adminUserRepository.GetAdminUserByUsernameAsync(username);
            if (adminUser is null)
                return null;

            return new AdminUserDataDto
            {
                Id = adminUser.Id,
                Username = adminUser.Username
            };
        }

        public async Task SeedAdminUserDataAsync()
        {
            if (await GetByUsernameAsync("admin") is null)
            {
                var username = "admin";
                var password = Crypto.HashPassword("imredone");

                await _adminUserRepository.InsertAsync(username, password);
            }
        }
    }
}
