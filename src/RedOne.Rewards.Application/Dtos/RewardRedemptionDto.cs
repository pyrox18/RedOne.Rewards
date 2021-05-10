using RedOne.Rewards.Domain.Entities;
using System;
using System.Text.Json.Serialization;

namespace RedOne.Rewards.Application.Dtos
{
    public class RewardRedemptionDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("rewardTitle")]
        public string RewardTitle { get; set; }

        [JsonPropertyName("pointsSpent")]
        public int PointsSpent { get; set; }

        [JsonPropertyName("extraCashSpent")]
        public int ExtraCashSpent { get; set; }

        [JsonPropertyName("redemptionDate")]
        public DateTimeOffset RedemptionDate { get; set; }

        public RewardRedemptionDto(RewardRedemption rewardRedemption)
        {
            Id = rewardRedemption.Id;
            RewardTitle = rewardRedemption.RewardTitle;
            PointsSpent = rewardRedemption.PointsSpent;
            ExtraCashSpent = rewardRedemption.ExtraCashSpent;
            RedemptionDate = rewardRedemption.RedemptionDate;
        }
    }
}
