using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedOne.Rewards.Application.Dtos;
using RedOne.Rewards.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace RedOne.Rewards.WebApi.Controllers.Consumer
{
    public class BannersController : BaseConsumerApiController
    {
        private readonly IBannerService _bannerService;

        public BannersController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets the current list of available banners",
            Tags = new[] { "Banners (Consumer)" })]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns banners", typeof(BannerDto))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Not authenticated")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Authenticated but not a consumer user")]
        public async Task<IActionResult> GetBanners()
        {
            var result = await _bannerService.GetBannersAsync();

            return new JsonResult(result);
        }
    }
}
