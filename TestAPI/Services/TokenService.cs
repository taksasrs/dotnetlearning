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
using StackExchange.Redis;

namespace TestAPI.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly string _secretKey;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;

        public TokenService(IConfiguration configuration, ITokenRepository tokenRepository, IUserRepository userRepository)
        {
            _configuration = configuration;
            _secretKey = configuration["JwtSettings:SecretKey"]!;
            _tokenRepository = tokenRepository;
            _userRepository = userRepository;
        }

        private const double JwtExpireHours = 1; // JWT expiration time
        private const double RefreshTokenExpireDays = 7; // Refresh token expiration time

        public async Task<string> GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    
                };
                var roles = await _userRepository.GetUserRoles(username);
                // "User","Manager","Admin"
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
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

        //public string GenerateJwtToken2(string user, IList<string> roles)
        //{
        //    var claims = new List<Claim>
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, user),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim(ClaimTypes.Name, user)
        //    };

        //    // Add roles as claims
        //    foreach (var role in roles)
        //    {
        //        claims.Add(new Claim(ClaimTypes.Role, role));
        //    }

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        issuer: "your_issuer",
        //        audience: "your_audience",
        //        claims: claims,
        //        expires: DateTime.Now.AddMinutes(60),
        //        signingCredentials: creds);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}


        public async Task<string> GenerateRefreshToken(string username)
        {
            //_tokenRepository.RemoveTokenExists(username);
            Token token = new()
            {
                Username = username,
                RefreshToken = GenerateRandomToken(),//"mydummyr3fr35ht0k3nand1l332s",//randomBytes.ToString()!,
                RefreshTokenExpiryTime = DateTime.Now.AddDays(RefreshTokenExpireDays)
            };


            await _tokenRepository.AddRefreshTokenAsync(token);
            return token.RefreshToken;
            
        }
        public async Task<string> GenerateRefreshTokenAndRemoveExists(string username)
        {
            await _tokenRepository.RemoveTokenExistsAsync(username);
            Token token = new()
            {
                Username = username,
                RefreshToken = GenerateRandomToken(),//"mydummyr3fr35ht0k3nand1l332s",//randomBytes.ToString()!,
                RefreshTokenExpiryTime = DateTime.Now.AddDays(RefreshTokenExpireDays)
            };


            await _tokenRepository.AddRefreshTokenAsync(token);
            return token.RefreshToken;

        }

        public bool ValidateRefreshToken(string refreshToken)
        {
            // Implement your logic to validate the refresh token
            // e.g., check if it exists in the database and hasn't expired

            return _tokenRepository.TokenValidate(refreshToken);
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

        public static string GenerateRandomToken()
        {
            byte[] randomBytes = new byte[64]; // 64 bits = 8 bytes
            RandomNumberGenerator.Fill(randomBytes);

            // Convert to Base64 string
            string token = Convert.ToBase64String(randomBytes);

            // Optional: remove any trailing padding characters ('=') for a cleaner token
            token = token.TrimEnd('=');

            return token;
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