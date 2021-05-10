using RedOne.Rewards.Domain.Models;
using System.Text.Json.Serialization;

namespace RedOne.Rewards.Application.Dtos
{
    public class ConsumerUserRewardInfoDto
    {
        [JsonPropertyName("totalPoints")]
        public int TotalPoints { get; set; }

        [JsonPropertyName("memberLevel")]
        public int MemberLevel { get; set; }

        [JsonPropertyName("memberLevelText")]
        public string MemberLevelText { get; set; }

        public ConsumerUserRewardInfoDto(ConsumerUserRewardInfoSpModel model)
        {
            TotalPoints = model.TotalPoints;
            MemberLevel = model.MemberLevel;
            MemberLevelText = model.MemberLevelText;
        }
    }
}
