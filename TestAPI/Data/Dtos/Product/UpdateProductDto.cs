using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Data.Dtos.Product
{
    public class UpdateProductDto
    {
        [Required]
        public int ProductId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public byte[]? Image { get; set; }

        public double Price { get; set; }

        public int Stock { get; set; }
    }
}