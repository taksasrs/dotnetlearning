using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Data.Dtos
{
    public class CreateShopDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string? Description { get; set; }

        public byte[]? Image { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}