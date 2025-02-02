using Microsoft.IdentityModel.Tokens;
using PetShop.Domain.Entities;
using PetShop.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Core
{
    public static class TokenService
    {
        //encriptando usuário utilizando JWT

        public static string GenerateToken(Users user)
        {
            var usertype = user.UserType.ToString("G");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("user_id", user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, usertype),
                    new Claim("RegistrationNumber", user.RegistrationNumber.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(JwtSettings.HourExpire),

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GetRegistrationNumberFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var RegistrationNumberClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "RegistrationNumber")?.Value;

            return RegistrationNumberClaim;

        }

        public static string GetEmailFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

            return emailClaim;

        }

        public static string GetIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var idClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;

            return idClaim;

        }

        public static string GetRoleFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwtToken = handler.ReadJwtToken(token);

            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

            return roleClaim;
        }

        //public static bool IsTokenCpfValid(string token, string expectedCpf)
        //{
        //    var cpfFromToken = GetCpfFromToken(token);
        //    return cpfFromToken == expectedCpf;
        //}


    }
}
