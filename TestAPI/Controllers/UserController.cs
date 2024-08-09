using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Models;
using TestAPI.Services;
using Microsoft.AspNetCore.Authorization;
using TestAPI.Data.Dtos.User;
using AutoMapper;

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
        public async Task<IActionResult> PostMovie(UserDto user)
        {
            //var user = _mapper.Map<User>(usermap);

            var createdUser = await _userService.CreateUser(user);
            if(createdUser.Success == false){
                return BadRequest(createdUser);
            }
            return Ok(createdUser);
        }
        // POST: api/User
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUser(string username)
        {
            var ret = await _userService.GetUser(username);
            if (ret.Data == null)
                return NotFound(ret);
            return Ok(ret);
        }
    }
}
