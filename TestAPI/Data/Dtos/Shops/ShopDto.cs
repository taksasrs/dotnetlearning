using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Models;

namespace TestAPI.Data.Dtos.Shops
{
    public class ShopDto
    {
    
        public virtual IEnumerable<Shop> content { get; set; }

        public virtual PaginateDto pageable { get; set; }

    }
}