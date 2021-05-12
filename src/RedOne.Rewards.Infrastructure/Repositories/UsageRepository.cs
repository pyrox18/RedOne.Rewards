using Dapper;
using Microsoft.Extensions.Configuration;
using RedOne.Rewards.Domain.Entities;
using RedOne.Rewards.Domain.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace RedOne.Rewards.Infrastructure.Repositories
{
    public class UsageRepository : BaseRepository, IUsageRepository
    {
        public UsageRepository(IConfiguration configuration) :
            base(configuration)
        {
        }

        public async Task<IEnumerable<Usage>> GetUsageForConsumerUserAsync(string phoneNumber)
        {
            using (var connection = DbConnection)
            {
                return await connection.QueryAsync<Usage>(
                    "GetConsumerUserUsage",
                    new { PhoneNumber = phoneNumber },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task SeedInsertAsync(Usage usage, string seedUserPhoneNumber = "018009999")
        {
            var consumerUserQuery = @"SELECT Id from ConsumerUser WHERE PhoneNumber = @PhoneNumber";

            var query = @"INSERT INTO `Usage`(ConsumerUserId, Title, CurrentUsage, UsageLimit, Unit)
                          VALUES (@ConsumerUserId, @Title, @CurrentUsage, @UsageLimit, @Unit)";

            using (var connection = DbConnection)
            {
                var userId = await connection.ExecuteScalarAsync<int>(consumerUserQuery, new { PhoneNumber = seedUserPhoneNumber });
                usage.ConsumerUserId = userId;

                await connection.ExecuteAsync(query, usage);
            }
        }
    }
}
