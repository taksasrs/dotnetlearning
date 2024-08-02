using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Models;
using TestAPI.Services;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> PostMovie(User user)
        {
            var createdUser = await _userService.CreateUser(user);
            return Ok(createdUser);
        }
        // POST: api/User
        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            var createdUser = await _userService.GetUser(id);
            return Ok(createdUser);
        }
    }
}
