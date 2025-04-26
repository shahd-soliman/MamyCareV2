using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MamyCare.Authentication
{
    public class JwtProvider(IOptions<Jwtoptions> jwtOptions) : IJwtProvider

    {
        private readonly Jwtoptions _jwtOptions = jwtOptions.Value;



        public (string Token, int Expiredate) GenerateJwtToken(ApplicationUser user)
        {
            Claim[] claims = [
                             new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                             new(JwtRegisteredClaimNames.Email, user.Email!),
                            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                             ];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
               expires: DateTime.UtcNow.AddDays(_jwtOptions.ExpireDays),
                signingCredentials: creds
            );
            var token = new JwtSecurityTokenHandler().WriteToken(Token);
            return (token, _jwtOptions.ExpireDays * 5);

        }
    }
}
