using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedOne.Rewards.Application.Dtos;
using RedOne.Rewards.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedOne.Rewards.WebApi.Controllers.Admin
{
    public class RewardsController : BaseAdminApiController
    {
        private readonly IRewardService _rewardService;

        public RewardsController(IRewardService rewardService)
        {
            _rewardService = rewardService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RewardDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> RewardsList()
        {
            var result = await _rewardService.GetRewardsAsync();

            return new JsonResult(result);
        }
    }
}
