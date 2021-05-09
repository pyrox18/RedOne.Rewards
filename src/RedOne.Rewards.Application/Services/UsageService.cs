using RedOne.Rewards.Application.Dtos;
using RedOne.Rewards.Application.Interfaces;
using RedOne.Rewards.Domain.Entities;
using RedOne.Rewards.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedOne.Rewards.Application.Services
{
    public class UsageService : IUsageService
    {
        private readonly IUsageRepository _usageRepository;

        public UsageService(IUsageRepository usageRepository)
        {
            _usageRepository = usageRepository;
        }

        public async Task<IEnumerable<UsageDto>> GetUsageForPhoneNumberAsync(string phoneNumber)
        {
            var result = await _usageRepository.GetUsageForConsumerUserAsync(phoneNumber);

            return result.Select(r => new UsageDto(r));
        }

        public async Task SeedUsageDataAsync()
        {
            var usageData = new List<Usage>
            {
                new Usage
                {
                    Title = "Internet",
                    CurrentUsage = 30,
                    UsageLimit = 100,
                    Unit = "GB"
                },
                new Usage
                {
                    Title = "Call",
                    CurrentUsage = 50,
                    UsageLimit = 500,
                    Unit = "minutes"
                },
                new Usage
                {
                    Title = "Text",
                    CurrentUsage = 20,
                    UsageLimit = 100,
                    Unit = "SMS"
                }
            };

            foreach (var usage in usageData)
            {
                await _usageRepository.SeedInsertAsync(usage, "018009999");
            }
        }
    }
}
