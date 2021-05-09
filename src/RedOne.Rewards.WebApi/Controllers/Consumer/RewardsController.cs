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
        [SwaggerOperation(Tags = new[] { "Rewards (Consumer)" })]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns rewards sorted by member level", typeof(IEnumerable<RewardDto>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Not authenticated")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Authenticated but not a consumer user")]
        public async Task<IActionResult> GetRewards()
        {
            var result = await _rewardService.GetRewardsAsync(true);

            return new JsonResult(result);
        }

        [HttpGet("user")]
        [SwaggerOperation(Tags = new[] { "Rewards (Consumer)" })]
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
        [SwaggerOperation(Tags = new[] { "Rewards (Consumer)" })]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns all member levels", typeof(IEnumerable<MemberLevelDto>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Not authenticated")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Authenticated but not a consumer user")]
        public async Task<IActionResult> GetMemberLevels()
        {
            var result = await _memberLevelService.GetMemberLevelsAsync();

            return new JsonResult(result);
        }
    }
}
