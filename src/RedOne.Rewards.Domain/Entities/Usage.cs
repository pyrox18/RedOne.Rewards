namespace RedOne.Rewards.Domain.Entities
{
    public class Usage : BaseEntity
    {
        public int ConsumerUserId { get; set; }
        public string Title { get; set; }
        public int CurrentUsage { get; set; }
        public int UsageLimit { get; set; }
        public string Unit { get; set; }
    }
}
