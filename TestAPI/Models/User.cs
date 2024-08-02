using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestAPI.Models ;

public partial class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string ChatId { get; set; }

    public DateTime? CreateAt { get; set; }

    //public virtual ICollection<Shop> Shops { get; set; } = new List<Shop>();
}
