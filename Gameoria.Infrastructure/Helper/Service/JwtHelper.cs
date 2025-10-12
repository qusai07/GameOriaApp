using GameOria.Domains.Entities.Identity;
using GameOria.Domains.Enums;
using GameOria.Infrastructure.Helper.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameOria.Infrastructure.Helper.Service
{
    public class JwtHelper
    {
        private readonly JwtSettings _jwtSettings;

        public JwtHelper(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public ClaimsPrincipal ValidateTokenAndGetPrincipal(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidAudience = _jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return principal;
            }
            catch
            {
                return null;
            }
        }
        public bool ValidateToken(string token, out string identityNumber)
        {
            identityNumber = null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            try
            {
                var claims = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidAudience = _jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                identityNumber = claims.FindFirst("IdentityNumber")?.Value;
                return true;
            }
            catch
            {
                return false;
            }
        }


        public string GenerateToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            if (user.Role == Roles.Organizer && !string.IsNullOrEmpty(user.IdentityNumber))
            {
                claims.Add(new Claim("IdentityNumber", user.IdentityNumber));
            }


            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
