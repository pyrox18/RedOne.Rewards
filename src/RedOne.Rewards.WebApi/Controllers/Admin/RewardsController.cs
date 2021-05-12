using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedOne.Rewards.Application.Dtos;
using RedOne.Rewards.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerOperation(
            Summary = "Gets the current list of available rewards",
            Tags = new[] { "Rewards (Admin)" })]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the current list of rewards", typeof(IEnumerable<RewardDto>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Not authenticated")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Authenticated but not an admin user")]
        public async Task<IActionResult> RewardsList()
        {
            var result = await _rewardService.GetRewardsAsync();

            return new JsonResult(result);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new reward",
            Tags = new[] { "Rewards (Admin)" })]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the created reward", typeof(RewardDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid body")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Not authenticated")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Authenticated but not an admin user")]
        public async Task<IActionResult> CreateReward([FromBody] CreateRewardDto dto)
        {
            var result = await _rewardService.CreateRewardAsync(dto);

            return new JsonResult(result);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deletes an existing reward",
            Tags = new[] { "Rewards (Admin)" })]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Indicates a successful deletion")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Not authenticated")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Authenticated but not an admin user")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Reward with the given ID was not found")]
        public async Task<IActionResult> DeleteReward([FromRoute] int id)
        {
            await _rewardService.DeleteRewardAsync(id);

            return NoContent();
        }
    }
}
