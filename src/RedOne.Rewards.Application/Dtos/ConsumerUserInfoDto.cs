using RedOne.Rewards.Domain.Entities;
using System.Text.Json.Serialization;

namespace RedOne.Rewards.Application.Dtos
{
    public class ConsumerUserInfoDto
    {
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("isRoamingActivated")]
        public bool IsRoamingActivated { get; set; }

        [JsonPropertyName("isIDDActivated")]
        public bool IsIDDActivated { get; set; }

        public ConsumerUserInfoDto(ConsumerUser consumerUser)
        {
            PhoneNumber = consumerUser.PhoneNumber;
            Name = consumerUser.Name;
            EmailAddress = consumerUser.EmailAddress;
            IsActive = consumerUser.IsActive;
            IsRoamingActivated = consumerUser.IsRoamingActivated;
            IsIDDActivated = consumerUser.IsIDDActivated;
        }
    }
}
