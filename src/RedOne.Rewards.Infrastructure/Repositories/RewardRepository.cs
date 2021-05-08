using RedOne.Rewards.Domain.Entities;
using RedOne.Rewards.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text;
using Dapper;

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
            var querySb = new StringBuilder(@"SELECT r.Title, r.Description, r.ExtraCashRequired, r.ExtraCashAmount, r.ExpiryDate, ml.Level AS MinimumMemberLevel
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
    }
}
