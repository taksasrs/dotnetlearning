using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Azure;
using TestAPI.Models;
using Microsoft.IdentityModel.Tokens;
using TestAPI.Repository;

namespace TestAPI.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly string _secretKey;
        private readonly ITokenRepository _tokenRepository;

        public TokenService(IConfiguration configuration, ITokenRepository tokenRepository)
        {
            _configuration = configuration;
            _secretKey = configuration["JwtSettings:SecretKey"]!;
            _tokenRepository = tokenRepository;
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

        public async Task<string> GenerateRefreshToken(string username,string oldToken = "")
        {
            var randomBytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
                // save token
                Token tokenModel = new Token()
                {
                    Username = username,
                    RefreshToken = randomBytes.ToString()!,
                    RefreshTokenExpiryTime = DateTime.Now.AddDays(RefreshTokenExpireDays)
                };
                if (string.IsNullOrEmpty(oldToken))
                    await _tokenRepository.DeleteRefreshTokenAsync(oldToken);
                await _tokenRepository.AddRefreshTokenAsync(tokenModel);
                return Convert.ToBase64String(randomBytes);
            }
        }

        public bool ValidateRefreshToken(string refreshToken)
        {
            // Implement your logic to validate the refresh token
            return _tokenRepository.TokenValidate(refreshToken);
        }

        //public static string GetUsernameFromExpiredToken(string token)
        //{
        //    // Decode the expired JWT to get the username
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var jwtToken = tokenHandler.ReadJwtToken(token);

        //    // Extract the username claim
        //    var usernameClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "username");

        //    return usernameClaim?.Value;
        //}

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