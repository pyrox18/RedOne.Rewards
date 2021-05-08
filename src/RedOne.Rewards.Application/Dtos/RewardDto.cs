using RedOne.Rewards.Domain.Entities;
using System;
using System.Text.Json.Serialization;

namespace RedOne.Rewards.Application.Dtos
{
    public class RewardDto
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("pointsRequired")]
        public int PointsRequired { get; set; }

        [JsonPropertyName("extraCashRequired")]
        public bool ExtraCashRequired { get; set; }

        [JsonPropertyName("extraCashAmount")]
        public bool ExtraCashAmount { get; set; }

        [JsonPropertyName("expiryDate")]
        public DateTimeOffset ExpiryDate { get; set; }

        [JsonPropertyName("minimumMemberLevel")]
        public int MinimumMemberLevel { get; set; }

        public RewardDto(Reward reward)
        {
            Title = reward.Title;
            Description = reward.Description;
            PointsRequired = reward.PointsRequired;
            ExtraCashRequired = reward.ExtraCashRequired;
            ExtraCashAmount = reward.ExtraCashAmount;
            ExpiryDate = reward.ExpiryDate;
            MinimumMemberLevel = reward.MinimumMemberLevel;
        }
    }
}
