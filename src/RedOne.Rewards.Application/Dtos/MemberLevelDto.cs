using RedOne.Rewards.Domain.Entities;
using System.Text.Json.Serialization;

namespace RedOne.Rewards.Application.Dtos
{
    public class MemberLevelDto
    {
        [JsonPropertyName("memberLevel")]
        public int MemberLevel { get; set; }

        [JsonPropertyName("memberLevelText")]
        public string MemberLevelText { get; set; }

        [JsonPropertyName("memberLevelThreshold")]
        public int MemberLevelThreshold { get; set; }

        [JsonPropertyName("memberLevelThresholdText")]
        public string MemberLevelThresholdText { get; set; }

        public MemberLevelDto(MemberLevel memberLevel)
        {
            MemberLevel = memberLevel.Level;
            MemberLevelText = memberLevel.LevelText;
            MemberLevelThreshold = memberLevel.Threshold;
            MemberLevelThresholdText = $"{memberLevel.Threshold} pts collected in the past 12 months";
        }
    }
}
