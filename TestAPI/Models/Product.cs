using System;
using System.Collections.Generic;

namespace TestAPI.Models ;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public byte[]? Image { get; set; }

    public double Price { get; set; }

    public int Stock { get; set; }

    public DateTime? CreateAt { get; set; }

    public int ShopId { get; set; }

    public virtual Shop Shop { get; set; } = null!;
}
