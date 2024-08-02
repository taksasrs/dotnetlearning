using System;
using System.Collections.Generic;

namespace TestAPI.Models ;

public partial class Shop
{
    public int ShopId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public byte[]? Image { get; set; }

    public DateTime? CreateAt { get; set; }

    public string Username { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual User User { get; set; } = null!;
}
