using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RedOne.Rewards.WebApi.Controllers.Consumer
{
    public class UserController : BaseConsumerApiController
    {
        [HttpGet("info")]
        public async Task<IActionResult> UserInfo()
        {
            return Ok();
        }
    }
}
