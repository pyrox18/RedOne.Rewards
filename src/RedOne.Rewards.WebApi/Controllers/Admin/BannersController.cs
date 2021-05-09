using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedOne.Rewards.Application.Dtos;
using RedOne.Rewards.Application.Interfaces;
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
        [ProducesResponseType(typeof(IEnumerable<BannerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> BannerList()
        {
            var result = await _bannerService.GetBannersAsync();

            return new JsonResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BannerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateBanner([FromBody] CreateBannerDto dto)
        {
            var result = await _bannerService.CreateBannerAsync(dto);

            return new JsonResult(result);
        }
    }
}
