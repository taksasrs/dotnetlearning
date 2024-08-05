using System;
using System.ComponentModel.DataAnnotations;

namespace TestAPI.Models
{
    public class TokenRequest
    {
        public string Username { get; set; }
        public string Otp { get; set; }
        //public string JwtToken { get; set; }
        //public string RefreshToken { get; set; }
    }

    public class TokenResponse
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
    public class TokenRefreshRequest
    {
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class Token
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }

}

