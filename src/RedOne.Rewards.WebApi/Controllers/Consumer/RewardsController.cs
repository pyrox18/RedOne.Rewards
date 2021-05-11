using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedOne.Rewards.Application.Dtos;
using RedOne.Rewards.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RedOne.Rewards.WebApi.Controllers.Consumer
{
    public class RewardsController : BaseConsumerApiController
    {
        private readonly IRewardService _rewardService;
        private readonly IMemberLevelService _memberLevelService;

        public RewardsController(
            IRewardService rewardService,
            IMemberLevelService memberLevelService)
        {
            _rewardService = rewardService;
            _memberLevelService = memberLevelService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets available rewards sorted by member level",
            Tags = new[] { "Rewards (Consumer)" })]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns rewards sorted by member level", typeof(IEnumerable<RewardDto>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Not authenticated")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Authenticated but not a consumer user")]
        public async Task<IActionResult> GetRewards()
        {
            var result = await _rewardService.GetRewardsAsync(true);

            return new JsonResult(result);
        }

        [HttpGet("user")]
        [SwaggerOperation(
            Summary = "Gets the consumer user's current reward info",
            Description = "Gets data on the consumer user's current total points and member level.",
            Tags = new[] { "Rewards (Consumer)" })]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns reward info for current user", typeof(ConsumerUserRewardInfoDto))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Not authenticated")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Authenticated but not a consumer user")]
        public async Task<IActionResult> GetRewardInfo()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            var result = await _rewardService.GetConsumerUserRewardInfoAsync(claim.Value);

            return new JsonResult(result);
        }

        [HttpGet("member-levels")]
        [SwaggerOperation(
            Summary = "Gets all available member levels",
            Tags = new[] { "Rewards (Consumer)" })]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns all member levels", typeof(IEnumerable<MemberLevelDto>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Not authenticated")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Authenticated but not a consumer user")]
        public async Task<IActionResult> GetMemberLevels()
        {
            var result = await _memberLevelService.GetMemberLevelsAsync();

            return new JsonResult(result);
        }

        [HttpPost("{id}/redeem")]
        [SwaggerOperation(
            Summary = "Redeems a reward for the consumer user",
            Tags = new[] { "Rewards (Consumer)" })]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Reward redemption successful")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Unable to redeem reward (see error message for description)")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Not authenticated")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Authenticated but not a consumer user")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Consumer user or reward not found")]
        public async Task<IActionResult> RedeemReward([FromRoute] int id)
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            await _rewardService.RedeemRewardAsync(claim.Value, id);

            return new NoContentResult();
        }

        [HttpGet("redemptions")]
        [SwaggerOperation(
            Summary = "Gets all previous reward redemptions",
            Tags = new[] { "Rewards (Consumer)" })]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the consumer user's previous reward redemptions", typeof(IEnumerable<RewardRedemptionDto>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Not authenticated")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Authenticated but not a consumer user")]
        public async Task<IActionResult> GetRewardRedemptions()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            var result = await _rewardService.GetConsumerUserRewardRedemptionsAsync(claim.Value);

            return new JsonResult(result);
        }
    }
}
