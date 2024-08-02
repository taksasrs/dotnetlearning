using System;
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
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class Token
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }

}

