using FluentValidation;
using RedOne.Rewards.Domain.Entities;
using System.Text.Json.Serialization;

namespace RedOne.Rewards.Application.Dtos
{
    public class CreateBannerDto
    {
        [JsonPropertyName("postCoverUrl")]
        public string PostCoverUrl { get; set; }

        [JsonPropertyName("postTitle")]
        public string PostTitle { get; set; }

        [JsonPropertyName("postShortDesc")]
        public string PostShortDesc { get; set; }

        [JsonPropertyName("postUrl")]
        public string PostUrl { get; set; }

        public Banner ToBanner()
        {
            return new Banner
            {
                PostCoverUrl = PostCoverUrl,
                PostTitle = PostTitle,
                PostShortDesc = PostShortDesc,
                PostUrl = PostUrl
            };
        }
    }

    public class CreateBannerDtoValidator : AbstractValidator<CreateBannerDto>
    {
        public CreateBannerDtoValidator()
        {
            RuleFor(x => x.PostCoverUrl)
                .NotEmpty();

            RuleFor(x => x.PostTitle)
                .NotEmpty();

            RuleFor(x => x.PostUrl)
                .NotEmpty();
        }
    }
}
