using RedOne.Rewards.Domain.Entities;
using RedOne.Rewards.Domain.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Collections.Generic;

namespace RedOne.Rewards.Infrastructure.Repositories
{
    public class MemberLevelRepository : BaseRepository, IMemberLevelRepository
    {
        public MemberLevelRepository(IConfiguration configuration) :
            base(configuration)
        {
        }

        public async Task InsertAsync(MemberLevel memberLevel)
        {
            var query = @"INSERT INTO MemberLevel(`Level`, LevelText, Threshold)
                          VALUES (@Level, @LevelText, @Threshold)";

            using (var connection = DbConnection)
            {
                await connection.ExecuteAsync(query, memberLevel);
            }
        }

        public async Task<bool> IsEmptyAsync()
        {
            var query = @"SELECT COUNT(*) FROM MemberLevel";

            using (var connection = DbConnection)
            {
                var count = await connection.ExecuteScalarAsync<int>(query);
                return count == 0;
            }
        }

        public async Task<MemberLevel> GetMemberLevelByLevelAsync(int level)
        {
            var query = @"SELECT * FROM MemberLevel WHERE `Level` = @Level LIMIT 1";

            using (var connection = DbConnection)
            {
                return await connection.QueryFirstOrDefaultAsync<MemberLevel>(query, new { Level = level });
            }
        }

        public async Task<IEnumerable<MemberLevel>> GetMemberLevelsAsync()
        {
            var query = @"SELECT * FROM MemberLevel
                          ORDER BY `Level`";

            using (var connection = DbConnection)
            {
                return await connection.QueryAsync<MemberLevel>(query);
            }
        }
    }
}
