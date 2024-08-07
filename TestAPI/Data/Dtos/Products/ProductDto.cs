using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Models;

namespace TestAPI.Data.Dtos.Products
{
    public class ProductDto
    {
         public virtual IEnumerable<Product> content { get; set; }

        public virtual PaginateDto pageable { get; set; }
    }
}