using RedOne.Rewards.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedOne.Rewards.Domain.Interfaces
{
    public interface IUsageRepository
    {
        Task SeedInsertAsync(Usage usage, string seedUserPhoneNumber = "018009999");
        Task<IEnumerable<Usage>> GetUsageForConsumerUserAsync(string phoneNumber);
    }
}
