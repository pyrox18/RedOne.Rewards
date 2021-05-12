using System;

namespace RedOne.Rewards.Domain.Entities
{
    public class Reward : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int PointsRequired { get; set; }
        public bool ExtraCashRequired { get; set; }
        public int? ExtraCashAmount { get; set; }
        public DateTimeOffset ExpiryDate { get; set; }
        public int MinimumMemberLevelId { get; set; }
        public int MinimumMemberLevel { get; set; }
    }
}
