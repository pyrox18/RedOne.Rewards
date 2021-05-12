using System.Text.Json.Serialization;

namespace RedOne.Rewards.Application.Dtos
{
    public class AuthenticateConsumerUserDto
    {
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
