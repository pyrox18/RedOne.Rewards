using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RedOne.Rewards.Application.Dtos;
using RedOne.Rewards.Application.Interfaces;
using RedOne.Rewards.WebApi.Configuration;
using RedOne.Rewards.WebApi.Dtos;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RedOne.Rewards.WebApi.Controllers.Admin
{
    public class AuthenticationController : BaseAdminApiController
    {
        private readonly IAdminUserService _adminUserService;
        private readonly AppSettings _appSettings;

        public AuthenticationController(IAdminUserService adminUserService, IOptions<AppSettings> appSettings)
        {
            _adminUserService = adminUserService;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateAdminUserDto dto)
        {
            var isAuthenticated = await _adminUserService.AuthenticateUserAsync(dto);
            if (!isAuthenticated)
            {
                return new JsonResult(new ErrorDto("Invalid credentials")) { StatusCode = StatusCodes.Status401Unauthorized };
            }

            var token = GenerateJwtToken(dto.Username);
            return new JsonResult(new TokenDto(token));
        }

        private string GenerateJwtToken(string username)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "admin")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
