using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedOne.Rewards.Application.Dtos;
using RedOne.Rewards.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedOne.Rewards.WebApi.Controllers.Admin
{
    public class BannersController : BaseAdminApiController
    {
        private readonly IBannerService _bannerService;

        public BannersController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets the current list of available banners",
            Tags = new[] { "Banners (Admin)" })]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the current list of banners", typeof(IEnumerable<BannerDto>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Not authenticated")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Authenticated but not an admin user")]
        public async Task<IActionResult> BannerList()
        {
            var result = await _bannerService.GetBannersAsync();

            return new JsonResult(result);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new banner",
            Tags = new[] { "Banners (Admin)" })]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the created banner", typeof(BannerDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid body")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Not authenticated")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Authenticated but not an admin user")]
        public async Task<IActionResult> CreateBanner([FromBody] CreateBannerDto dto)
        {
            var result = await _bannerService.CreateBannerAsync(dto);

            return new JsonResult(result);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deletes an existing banner",
            Tags = new[] { "Banners (Admin)" })]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Indicates a successful deletion")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Not authenticated")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Authenticated but not an admin user")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Banner with the given ID was not found")]
        public async Task<IActionResult> DeleteBanner([FromRoute] int id)
        {
            await _bannerService.DeleteBannerAsync(id);

            return NoContent();
        }
    }
}
