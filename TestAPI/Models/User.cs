using System;
using System.ComponentModel.DataAnnotations;

namespace TestAPI.Models
{
	public class User
	{
		// public int Id { get; set; }
        [Required]
        [Key]
        public string Username { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        public string ChatId { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}

