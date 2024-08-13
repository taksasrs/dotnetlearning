using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestAPI.Models ;

public partial class User
{
    [Key]
    public string Username { get; set; }
    [Required]
    public byte[] Password { get; set; }
    public byte[] PasswordSalt { get; set; }
    [Required]
    public string ChatId { get; set; }
    public DateTime? CreateAt { get; set; }

    public virtual ICollection<Shop> Shops { get; set; } = new List<Shop>();
    public virtual ICollection<UserRole> Roles { get; set; } = new List<UserRole>();
}

public class UserRole
{
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Role { get; set; }
    public virtual User User { get; set; } = null!;
}