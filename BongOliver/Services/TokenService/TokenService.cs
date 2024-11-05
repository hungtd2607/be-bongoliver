using BongOliver.Constants;
using BongOliver.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BongOliver.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        public TokenService(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }
        public string GetTokenData(string type)
        {
            var accessToken = _contextAccessor.HttpContext.GetTokenAsync("access_token").Result;
            var handler = new JwtSecurityTokenHandler();
            var data = handler.ReadJwtToken(accessToken).Claims.First(claim => claim.Type == type).Value;
            return data;
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("username", user.Username),
                new Claim("id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.FirstName + " " + user.LastName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };
            
            
            if (user.RoleId == Constant.ROLE_ADMIN)
                claims.Add(new Claim(ClaimTypes.Role, Constant.ROLE_NAME_ADMIN));
            else
            if (user.RoleId == Constant.ROLE_STYLIST)
                claims.Add(new Claim(ClaimTypes.Role, Constant.ROLE_NAME_STYLIST));
            else
                claims.Add(new Claim(ClaimTypes.Role, Constant.ROLE_NAME_USER));

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenKey"]));
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    symmetricKey, SecurityAlgorithms.HmacSha256Signature)
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }
    }
}
