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
    public class UsageController : BaseConsumerApiController
    {
        private readonly IUsageService _usageService;

        public UsageController(IUsageService usageService)
        {
            _usageService = usageService;
        }

        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Usage (Consumer)" })]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the consumer user's usage information", typeof(IEnumerable<UsageDto>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Not authenticated")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Authenticated but not a consumer user")]
        public async Task<IActionResult> GetUsage()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            var result = await _usageService.GetUsageForPhoneNumberAsync(claim.Value);

            return new JsonResult(result);
        }
    }
}
