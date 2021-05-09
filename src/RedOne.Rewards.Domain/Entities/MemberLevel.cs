namespace RedOne.Rewards.Domain.Entities
{
    public class MemberLevel : BaseEntity
    {
        public int Level { get; set; }
        public string LevelText { get; set; }
        public int Threshold { get; set; }
    }
}
