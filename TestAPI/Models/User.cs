using System;
using System.Collections.Generic;

namespace TestAPI.Models ;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string ChatId { get; set; } = null!;

    public DateTime? CreateAt { get; set; }

    public virtual ICollection<Shop> Shops { get; set; } = new List<Shop>();
}
