using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedOne.Rewards.Application.Dtos;
using RedOne.Rewards.Application.Interfaces;
using RedOne.Rewards.WebApi.Authentication;
using RedOne.Rewards.WebApi.Dtos;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace RedOne.Rewards.WebApi.Controllers.Admin
{
    public class AuthenticationController : BaseAdminApiController
    {
        private readonly IAdminUserService _adminUserService;
        private readonly ITokenService _tokenService;

        public AuthenticationController(IAdminUserService adminUserService, ITokenService tokenService)
        {
            _adminUserService = adminUserService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Tags = new[] { "Authentication (Admin)" })]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateAdminUserDto dto)
        {
            var isAuthenticated = await _adminUserService.AuthenticateUserAsync(dto);
            if (!isAuthenticated)
            {
                return new JsonResult(new ErrorDto("Invalid credentials")) { StatusCode = StatusCodes.Status401Unauthorized };
            }

            var token = _tokenService.GenerateAdminToken(dto.Username);
            return new JsonResult(new TokenDto(token));
        }
    }
}
