using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Models;
using TestAPI.Services;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly TokenService _tokenService;

        public AuthController(AuthService authService, TokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost("/GenerateToken")]
        public async Task<IActionResult> GenerateToken([FromBody] TokenRequest tokenRequest)
        {
            // Validate OTP
            if (!await _authService.VerifyOtpAsync(tokenRequest.Username, tokenRequest.Otp))
            {
                return Unauthorized();
            }
            TokenResponse dat = new();
            // Generate new tokens
            dat.JwtToken = _tokenService.GenerateJwtToken(tokenRequest.Username);
            dat.RefreshToken = await _tokenService.GenerateRefreshToken(tokenRequest.Username);

            return Ok(dat);
        }
        [HttpPost("/RefreshToken")]
        public async Task<IActionResult> Refresh(TokenRefreshRequest tokenResponse)
        {
            // Verify refresh token (validate against the stored token)
            if (!_tokenService.ValidateRefreshToken(tokenResponse.RefreshToken))
            {
                return Unauthorized();
            }

            // For demonstration, let's just generate a new access token
            var newJwtToken = _tokenService.GenerateJwtToken(tokenResponse.Username);
            var newRefreshToken = await _tokenService.GenerateRefreshToken(tokenResponse.Username);
            var response = new TokenResponse
            {
                JwtToken = newJwtToken,
                RefreshToken = newRefreshToken
            };

        //     return Ok(response);
        // }
        [HttpPost]
        [Route("/GenerateOtp")] //Login step
        public async Task<IActionResult> GenerateOtp(string username,string password)
        {
            var ret = await _authService.VerifyLoginAsync(username, password);
            if (!ret.Success)
            {
                return BadRequest(ret);
            }
            
            return Ok(ret);
        }
    }


    
}
