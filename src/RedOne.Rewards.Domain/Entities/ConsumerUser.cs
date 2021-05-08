namespace RedOne.Rewards.Domain.Entities
{
    public class ConsumerUser : BaseEntity
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsRoamingActivated { get; set; }
        public bool IsIDDActivated { get; set; }
        public string EmailAddress { get; set; }
        public int TotalRewardPoints { get; set; }
    }
}
