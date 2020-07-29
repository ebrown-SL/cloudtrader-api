using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CloudTrader.Api.Service.Interfaces;
using CloudTrader.Api.Service.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CloudTrader.Api.Service.Helpers
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        private readonly JwtTokenOptions _options;

        public JwtTokenGenerator(IOptions<JwtTokenOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(int id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public int DecodeToken(string tokenString)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(tokenString) as JwtSecurityToken;

            var stringClaimValue = securityToken.Claims
                .First(claim => claim.Type == ClaimTypes.Name)
                .Value;

            return int.Parse(stringClaimValue);
        }
    }
}
