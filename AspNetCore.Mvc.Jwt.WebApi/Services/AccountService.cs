using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Mvc.Jwt.WebApi.Services
{
    public interface IAccountService
    {
        Task<string> Authenticate(string username, string password);
    }

    public class AccountService : IAccountService
    {
        private IUserService userService;

        public AccountService(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<string> Authenticate(string username, string password)
        {
            // Assume ClientService as source of truth
            var client = await this.userService.GetByName(username);

            if (client == null)
                return null;

            var signingKey = Encoding.Default.GetBytes("ValidTokenSigningKey");
            var expiryDuration = 15;
            var now = DateTime.UtcNow;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                IssuedAt = now,
                NotBefore = now,
                Expires = now.AddMinutes(expiryDuration),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userid", client.Id),
                    new Claim(ClaimTypes.Name, client.Name),
                    new Claim(ClaimTypes.Email, client.Email),
                    new Claim(ClaimTypes.Role, client.Role)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);

            return token;
        }
    }
}
