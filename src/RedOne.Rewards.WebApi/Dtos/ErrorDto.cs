using System.Text.Json.Serialization;

namespace RedOne.Rewards.WebApi.Dtos
{
    public class ErrorDto
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        public ErrorDto(string message)
        {
            Message = message;
        }
    }
}
