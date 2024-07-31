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

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("/GenerateOtp")] //Login step
        public async Task<IActionResult> GenerateOtp(string username,string password)
        {
            var ret = await _authService.VerifyLoginAsync(username, password);
            if (!ret.Success)
            {
                return Unauthorized(ret);
            }
            return Ok(ret);
        }
    }
}
