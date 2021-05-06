namespace RedOne.Rewards.Domain.Entities
{
    public class AdminUser : BaseEntity
    {
        public string Username { get; }
        public string Password { get; }

        public AdminUser(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
