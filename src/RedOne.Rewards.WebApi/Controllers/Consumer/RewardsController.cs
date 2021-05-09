using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedOne.Rewards.Application.Dtos;
using RedOne.Rewards.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedOne.Rewards.WebApi.Controllers.Consumer
{
    public class RewardsController : BaseConsumerApiController
    {
        private readonly IRewardService _rewardService;

        public RewardsController(IRewardService rewardService)
        {
            _rewardService = rewardService;
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
    }
}
