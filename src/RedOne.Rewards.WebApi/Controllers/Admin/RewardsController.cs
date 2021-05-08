using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RedOne.Rewards.WebApi.Controllers.Admin
{
    public class RewardsController : BaseAdminApiController
    {
        [HttpGet]
        public async Task<IActionResult> RewardsList()
        {
            return Ok();
        }
    }
}
