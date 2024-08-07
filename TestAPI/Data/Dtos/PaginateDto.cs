using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Data.Dtos
{
    public class PaginateDto
    {
        public int totalPages {get; set;}
        public int currentPage {get; set;}
        public int sizePages {get; set;}
    }
}