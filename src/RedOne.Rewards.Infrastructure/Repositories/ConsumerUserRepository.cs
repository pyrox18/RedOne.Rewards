using Dapper;
using Microsoft.Extensions.Configuration;
using RedOne.Rewards.Domain.Entities;
using RedOne.Rewards.Domain.Interfaces;
using System.Threading.Tasks;

namespace RedOne.Rewards.Infrastructure.Repositories
{
    public class ConsumerUserRepository : BaseRepository, IConsumerUserRepository
    {
        public ConsumerUserRepository(IConfiguration configuration) :
            base(configuration)
        {
        }

        public async Task<ConsumerUser> GetConsumerUserByPhoneNumberAsync(string phoneNumber)
        {
            var query = "SELECT * FROM ConsumerUser WHERE PhoneNumber = @PhoneNumber";

            using (var connection = DbConnection)
            {
                return await connection.QuerySingleOrDefaultAsync<ConsumerUser>(query, new
                {
                    PhoneNumber = phoneNumber
                });
            }
        }

        public async Task InsertAsync(ConsumerUser consumerUser)
        {
            var query = @"INSERT INTO ConsumerUser(PhoneNumber, `Password`, `Name`, IsActive, IsRoamingActivated, IsIDDActivated, EmailAddress, TotalRewardPoints)
                          VALUES (@PhoneNumber, @Password, @Name, @IsActive, @IsRoamingActivated, @IsIDDActivated, @EmailAddress, @TotalRewardPoints)";

            using (var connection = DbConnection)
            {
                await connection.ExecuteAsync(query, consumerUser);
            }
        }
    }
}
