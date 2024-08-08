using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Data.Dtos.Products
{
    public class UpdateProductDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public byte[]? Image { get; set; }
        
        [Required]
        public double Price { get; set; }
        
        [Required]
        public int Stock { get; set; }

        [Required]
        public int ShopId { get; set;}

        public string? ImageName { get; set;}
    }
}