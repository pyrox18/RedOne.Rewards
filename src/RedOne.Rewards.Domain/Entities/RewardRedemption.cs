using System;

namespace RedOne.Rewards.Domain.Entities
{
    public class RewardRedemption : BaseEntity
    {
        public string RewardTitle { get; set; }
        public int PointsSpent { get; set; }
        public int ExtraCashSpent { get; set; }
        public DateTimeOffset RedemptionDate { get; set; }
    }
}
