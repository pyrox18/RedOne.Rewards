using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedOne.Rewards.Application.Dtos;
using RedOne.Rewards.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RedOne.Rewards.WebApi.Controllers.Consumer
{
    public class UserController : BaseConsumerApiController
    {
        private readonly IConsumerUserService _consumerUserService;

        public UserController(IConsumerUserService consumerUserService)
        {
            _consumerUserService = consumerUserService;
        }

        [HttpGet("info")]
        [SwaggerOperation(Tags = new[] { "User (Consumer)" })]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the consumer user's information", typeof(ConsumerUserInfoDto))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Not authenticated")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Authenticated but not a consumer user")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Consumer user with phone number not found")]
        public async Task<IActionResult> UserInfo()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            var result = await _consumerUserService.GetConsumerUserInfoAsync(claim.Value);

            return new JsonResult(result);
        }
    }
}
