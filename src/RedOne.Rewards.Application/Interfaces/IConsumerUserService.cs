using RedOne.Rewards.Application.Dtos;
using System.Threading.Tasks;

namespace RedOne.Rewards.Application.Interfaces
{
    public interface IConsumerUserService
    {
        Task<bool> AuthenticateUserAsync(AuthenticateConsumerUserDto dto);
        Task<ConsumerUserInfoDto> GetConsumerUserInfoAsync(string phoneNumber);
        Task SeedConsumerUserDataAsync();
    }
}
