namespace RedOne.Rewards.WebApi.Authentication
{
    public interface ITokenService
    {
        string GenerateConsumerToken(string phoneNumber);
        string GenerateAdminToken(string username);
    }
}
