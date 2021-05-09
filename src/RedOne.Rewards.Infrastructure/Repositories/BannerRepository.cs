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

        public async Task<Banner> InsertAsync(Banner banner)
        {
            var query = @"INSERT INTO Banner(PostCoverUrl, PostTitle, PostShortDesc, PostUrl)
                          VALUES (@PostCoverUrl, @PostTitle, @PostShortDesc, @PostUrl);
                          SELECT LAST_INSERT_ID();";
            var returnQuery = @"SELECT * FROM Banner
                                WHERE Id = @Id";

            using (var connection = DbConnection)
            {
                var id = await connection.ExecuteScalarAsync<int>(query, banner);
                return await connection.QueryFirstOrDefaultAsync<Banner>(returnQuery, new { Id = id });
            }
        }
    }
}
