using EmpDepRoleFulstackProjectJun13.Models.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmpDepRoleFulstackProjectJun13.Services
{
    public class tokenService:ITokenService
        {
            private readonly IConfiguration _configuration;

            public tokenService(IConfiguration configuration)
            {
                _configuration = configuration;
            }

        public string GenerateToken(User user)
            {
               
                var claims = new[]
                {
                  new Claim(ClaimTypes.Name, user.Username),
                  new Claim(ClaimTypes.Role, user.Role ?? "User")
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }

