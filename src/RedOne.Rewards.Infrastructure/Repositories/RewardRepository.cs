using RedOne.Rewards.Domain.Entities;
using RedOne.Rewards.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text;
using Dapper;
using RedOne.Rewards.Domain.Models;
using System.Data;

namespace RedOne.Rewards.Infrastructure.Repositories
{
    public class RewardRepository : BaseRepository, IRewardRepository
    {
        public RewardRepository(IConfiguration configuration) :
            base(configuration)
        {
        }

        public async Task<IEnumerable<Reward>> GetRewardsAsync(bool sortByMemberLevel = false)
        {
            var querySb = new StringBuilder(@"SELECT r.Id, r.Title, r.`Description`, r.PointsRequired, r.ExtraCashRequired,
                                              r.ExtraCashAmount, r.ExpiryDate, ml.Level AS MinimumMemberLevel
                                              FROM Reward r
                                              JOIN MemberLevel ml ON r.MinimumMemberLevelId = ml.Id");
            if (sortByMemberLevel)
            {
                querySb.Append(" ORDER BY ml.Level ASC");
            }

            using (var connection = DbConnection)
            {
                return await connection.QueryAsync<Reward>(querySb.ToString());
            }
        }

        public async Task<Reward> InsertAsync(Reward reward)
        {
            var query = @"INSERT INTO Reward(Title, `Description`, PointsRequired, ExtraCashRequired, ExtraCashAmount, ExpiryDate, MinimumMemberLevelId)
                          VALUES (@Title, @Description, @PointsRequired, @ExtraCashRequired, @ExtraCashAmount, @ExpiryDate, @MinimumMemberLevelId);
                          SELECT LAST_INSERT_ID();";
            var returnQuery = @"SELECT r.Id, r.Title, r.Description, r.PointsRequired, r.ExtraCashRequired,
                                r.ExtraCashAmount, r.ExpiryDate, ml.Level AS MinimumMemberLevel
                                FROM Reward r
                                JOIN MemberLevel ml ON r.MinimumMemberLevelId = ml.Id
                                WHERE r.Id = @Id";

            using (var connection = DbConnection)
            {
                var id = await connection.ExecuteScalarAsync<int>(query, reward);
                return await connection.QueryFirstOrDefaultAsync<Reward>(returnQuery, new { Id = id });
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var query = @"SELECT EXISTS(SELECT true FROM Reward WHERE Id = @Id)";

            using (var connection = DbConnection)
            {
                return await connection.ExecuteScalarAsync<bool>(query, new { Id = id });
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            var query = @"DELETE FROM Reward where Id = @Id";

            using (var connection = DbConnection)
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }

        public async Task<ConsumerUserRewardInfoSpModel> GetConsumerUserRewardInfoAsync(string phoneNumber)
        {
            using (var connection = DbConnection)
            {
                return await connection.QuerySingleOrDefaultAsync<ConsumerUserRewardInfoSpModel>(
                    "GetUserRewardInfo",
                    new { PhoneNumber = phoneNumber },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
