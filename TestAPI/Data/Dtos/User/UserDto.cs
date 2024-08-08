using System;
using System.ComponentModel.DataAnnotations;

namespace TestAPI.Data.Dtos.User
{
	public class UserDto
	{
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ChatId { get; set; }

        public List<string> Roles { get; set; } = new List<string>();
      
    }
}

