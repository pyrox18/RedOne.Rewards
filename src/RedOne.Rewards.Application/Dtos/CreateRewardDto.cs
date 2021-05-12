using FluentValidation;
using RedOne.Rewards.Domain.Entities;
using RedOne.Rewards.Domain.Interfaces;
using System;
using System.Text.Json.Serialization;

namespace RedOne.Rewards.Application.Dtos
{
    public class CreateRewardDto
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("pointsRequired")]
        public int PointsRequired { get; set; }

        [JsonPropertyName("extraCashRequired")]
        public bool ExtraCashRequired { get; set; }

        [JsonPropertyName("extraCashAmount")]
        public int? ExtraCashAmount { get; set; }

        [JsonPropertyName("expiryDate")]
        public DateTimeOffset ExpiryDate { get; set; }

        [JsonPropertyName("minimumMemberLevel")]
        public int MinimumMemberLevel { get; set; }

        public Reward ToReward()
        {
            return new Reward
            {
                Title = Title,
                Description = Description,
                PointsRequired = PointsRequired,
                ExtraCashRequired = ExtraCashRequired,
                ExtraCashAmount = ExtraCashAmount,
                ExpiryDate = ExpiryDate,
                MinimumMemberLevel = MinimumMemberLevel
            };
        }
    }

    public class CreateRewardDtoValidator : AbstractValidator<CreateRewardDto>
    {
        public CreateRewardDtoValidator(IMemberLevelRepository memberLevelRepository)
        {
            RuleFor(x => x.Title)
                .NotEmpty();

            RuleFor(x => x.Description)
                .NotEmpty();

            RuleFor(x => x.PointsRequired)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.ExtraCashAmount)
                .Must((dto, amount) =>
                {
                    if (dto.ExtraCashRequired)
                        return amount.HasValue && amount.Value > 0;

                    return true;
                })
                .WithMessage("Extra cash amount must have a value greater than 0 if extraCashRequired is set to true.");

            RuleFor(x => x.ExpiryDate)
                .GreaterThanOrEqualTo(DateTimeOffset.UtcNow);

            RuleFor(x => x.MinimumMemberLevel)
                .MustAsync(async (level, cancellationToken) =>
                {
                    var memberLevel = await memberLevelRepository.GetMemberLevelByLevelAsync(level);
                    return !(memberLevel is null);
                })
                .WithMessage("Member level does not exist.");
        }
    }
}
