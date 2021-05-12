using System.Text.Json.Serialization;

namespace RedOne.Rewards.Application.Dtos
{
    public class AuthenticateAdminUserDto
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
