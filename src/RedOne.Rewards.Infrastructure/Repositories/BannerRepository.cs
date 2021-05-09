using RedOne.Rewards.Domain.Entities;
using RedOne.Rewards.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace RedOne.Rewards.Infrastructure.Repositories
{
    public class BannerRepository : BaseRepository, IBannerRepository
    {
        public BannerRepository(IConfiguration configuration) :
            base(configuration)
        {
        }

        public async Task<IEnumerable<Banner>> GetAllAsync()
        {
            var query = @"SELECT * FROM Banner";

            using (var connection = DbConnection)
            {
                return await connection.QueryAsync<Banner>(query);
            }
        }
    }
}
