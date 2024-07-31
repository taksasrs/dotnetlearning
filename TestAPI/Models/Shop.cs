using System;
using System.ComponentModel.DataAnnotations;

namespace TestAPI.Models
{
	public class Shop
	{
		public int ShopId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImagePath { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string UserCreate {get; set;}
    }
}

