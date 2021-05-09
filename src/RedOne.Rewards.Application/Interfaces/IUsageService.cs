using RedOne.Rewards.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedOne.Rewards.Application.Interfaces
{
    public interface IUsageService
    {
        Task SeedUsageDataAsync();
        Task<IEnumerable<UsageDto>> GetUsageForPhoneNumberAsync(string phoneNumber);
    }
}
