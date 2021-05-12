using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedOne.Rewards.Application.Dtos;
using RedOne.Rewards.Application.Interfaces;
using RedOne.Rewards.WebApi.Authentication;
using RedOne.Rewards.WebApi.Dtos;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace RedOne.Rewards.WebApi.Controllers.Consumer
{
    public class AuthenticationController : BaseConsumerApiController
    {
        private readonly IConsumerUserService _consumerUserService;
        private readonly ITokenService _tokenService;

        public AuthenticationController(
            IConsumerUserService consumerUserService,
            ITokenService tokenService)
        {
            _consumerUserService = consumerUserService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [SwaggerOperation(
            Summary = "Authenticates a consumer user with a bearer token",
            Tags = new[] { "Authentication (Consumer)" })]
        [SwaggerResponse(StatusCodes.Status200OK, "Authentication token for the consumer user", typeof(TokenDto))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Invalid user credentials", typeof(ErrorDto))]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateConsumerUserDto dto)
        {
            var isAuthenticated = await _consumerUserService.AuthenticateUserAsync(dto);
            if (!isAuthenticated)
            {
                return new JsonResult(new ErrorDto("Invalid credentials")) { StatusCode = StatusCodes.Status401Unauthorized };
            }

            var token = _tokenService.GenerateConsumerToken(dto.PhoneNumber);
            return new JsonResult(new TokenDto(token));
        }
    }
}
