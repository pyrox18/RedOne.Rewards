using System.Text.Json.Serialization;

namespace RedOne.Rewards.WebApi.Dtos
{
    public class TokenDto
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        public TokenDto(string token)
        {
            Token = token;
        }
    }
}
