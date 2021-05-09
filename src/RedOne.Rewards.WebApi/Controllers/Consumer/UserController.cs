using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace RedOne.Rewards.WebApi.Controllers.Consumer
{
    public class UserController : BaseConsumerApiController
    {
        [HttpGet("info")]
        [SwaggerOperation(Tags = new[] { "User (Consumer)" })]
        public async Task<IActionResult> UserInfo()
        {
            return Ok();
        }
    }
}
