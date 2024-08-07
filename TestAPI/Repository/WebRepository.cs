using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestAPI.Models;
using System.Transactions;
using System.Data.Entity;
using AutoMapper;


namespace TestAPI.Repository
{
    public partial class WebRepository : IWebRepository
    {
        private readonly EcommerceContext _context;

         private readonly IMapper _mapper;

        public WebRepository(EcommerceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
