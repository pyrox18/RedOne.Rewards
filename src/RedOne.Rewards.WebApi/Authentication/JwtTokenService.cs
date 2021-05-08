using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RedOne.Rewards.WebApi.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RedOne.Rewards.WebApi.Authentication
{
    public class JwtTokenService : ITokenService
    {
        private readonly AppSettings _appSettings;

        public JwtTokenService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GenerateAdminToken(string username)
        {
            return GenerateJwtToken(username, "admin");
        }

        public string GenerateConsumerToken(string phoneNumber)
        {
            return GenerateJwtToken(phoneNumber, "consumer");
        }

        private string GenerateJwtToken(string username, string role)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
