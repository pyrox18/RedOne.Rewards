using RedOne.Rewards.Domain.Entities;
using System.Text.Json.Serialization;

namespace RedOne.Rewards.Application.Dtos
{
    public class BannerDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("postCoverUrl")]
        public string PostCoverUrl { get; set; }

        [JsonPropertyName("postTitle")]
        public string PostTitle { get; set; }

        [JsonPropertyName("postShortDesc")]
        public string PostShortDesc { get; set; }

        [JsonPropertyName("postUrl")]
        public string PostUrl { get; set; }

        public BannerDto(Banner banner)
        {
            Id = banner.Id;
            PostCoverUrl = banner.PostCoverUrl;
            PostTitle = banner.PostTitle;
            PostShortDesc = banner.PostShortDesc;
            PostUrl = banner.PostUrl;
        }
    }
}
