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
        private readonly UserService _userService;

        public AuthController(AuthService authService, TokenService tokenService, UserService userService)
        {
            _authService = authService;
            _tokenService = tokenService;
            _userService = userService;
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
            var roles = await _userService.GetRoles(tokenRequest.Username);
            dat.JwtToken = _tokenService.GenerateJwtToken(tokenRequest.Username,roles);
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
            var roles = await _userService.GetRoles(tokenResponse.Username);
            var newJwtToken = _tokenService.GenerateJwtToken(tokenResponse.Username,roles);
            var newRefreshToken = await _tokenService.GenerateRefreshTokenAndRemoveExists(tokenResponse.Username);
            var response = new TokenResponse
            {
                JwtToken = newJwtToken,
                RefreshToken = newRefreshToken
            };

            return Ok(response);
        }
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
