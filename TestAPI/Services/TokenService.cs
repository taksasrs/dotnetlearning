using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Azure;
using Microsoft.IdentityModel.Tokens;
using TestAPI.Repository;

namespace TestAPI.Services
{
    public interface ITokenService
    {
    
    }
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly string _secretKey;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _secretKey = configuration["JwtSettings:SecretKey"]!;
        }

        private const double JwtExpireHours = 1; // JWT expiration time
        private const double RefreshTokenExpireDays = 7; // Refresh token expiration time

        public string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            try
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, username)
                    }),
                    Expires = DateTime.UtcNow.AddHours(JwtExpireHours),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch
            {
                throw;
            }
        }

        public string GenerateRefreshToken(string username)
        {
            var randomBytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

        public bool ValidateRefreshToken(string refreshToken)
        {
            // Implement your logic to validate the refresh token
            // e.g., check if it exists in the database and hasn't expired

            return true;
        }

        public static string GetUsernameFromExpiredToken(string token)
        {
            // Decode the expired JWT to get the username
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Extract the username claim
            var usernameClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "username");

            return usernameClaim?.Value;
        }

        //public void SetRefreshTokenCookie(string refreshToken)
        //{
        //    var cookieOptions = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Secure = true, // Ensure the cookie is only sent over HTTPS
        //        Expires = DateTime.UtcNow.AddDays(30) // Set appropriate expiry time
        //    };

        //    Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        //}
    }
}