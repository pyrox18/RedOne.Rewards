using RedOne.Rewards.Domain.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using System.Collections.Generic;
using RedOne.Rewards.Domain.Entities;

namespace RedOne.Rewards.Infrastructure.Repositories
{
    public class RewardRedemptionRepository : BaseRepository, IRewardRedemptionRepository
    {
        public RewardRedemptionRepository(IConfiguration configuration) :
            base(configuration)
        {
        }

        public async Task<IEnumerable<RewardRedemption>> GetRewardRedemptionsAsync(string userPhoneNumber)
        {
            var query = @"SELECT rr.Id, rr.RewardTitle, rr.PointsSpent, rr.ExtraCashSpent, rr.RedemptionDate
                          FROM RewardRedemption rr
                          JOIN ConsumerUser cu ON cu.Id = rr.ConsumerUserId
                          WHERE cu.PhoneNumber = @PhoneNumber
                          ORDER BY rr.RedemptionDate DESC";

            using (var connection = DbConnection)
            {
                return await connection.QueryAsync<RewardRedemption>(query, new { PhoneNumber = userPhoneNumber });
            }
        }

        /// <summary>
        /// Runs a stored procedure to redeem a reward for a consumer user.
        /// </summary>
        /// <param name="userPhoneNumber">The consumer user's phone number.</param>
        /// <param name="rewardId">The reward ID to redeem.</param>
        /// <returns>An integer status code. 0 = successful redemption; -10 = Consumer user not found; -20 = Consumer user has 0 points; -30 = Consumer user does not meet minimum member level for reward; -40 = Consumer user does not have enough points; -50 = Reward not found</returns>
        public async Task<int> RedeemRewardAsync(string userPhoneNumber, int rewardId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PhoneNumber", userPhoneNumber);
            parameters.Add("@RewardId", rewardId);
            parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = DbConnection)
            {
                await connection.ExecuteAsync("RedeemReward", parameters, commandType: CommandType.StoredProcedure);
            }

            return parameters.Get<int>("@ReturnValue");
        }
    }
}
