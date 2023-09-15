using JwtAspNet.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAspNet.Services
{
    public class TokenService
    {
        public string Create(User user)
        {
            var handler = new JwtSecurityTokenHandler();
            var privateKey = Encoding.ASCII.GetBytes(Configuration.PrivateKey);


            var crendentials = new SigningCredentials(
                key: new SymmetricSecurityKey(privateKey), 
                algorithm: SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor {
                SigningCredentials = crendentials,
                Expires = DateTime.UtcNow.AddHours(2),
                Subject = GenerateClaims(user)
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }


        private static ClaimsIdentity GenerateClaims(User user)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim("id", user.Id.ToString()));
            ci.AddClaim(new Claim(ClaimTypes.Name, user.Email));
            ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            ci.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
            ci.AddClaim(new Claim("image", user.Image));

            foreach (var role in user.Roles)
            {
                ci.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return ci;
        }
    }
}
