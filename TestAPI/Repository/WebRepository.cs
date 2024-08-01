using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestAPI.Models;
using System.Transactions;
using System.Data.Entity;


namespace TestAPI.Repository
{
    public partial class WebRepository : IWebRepository
    {
        private readonly EcommerceContext _context;

        public WebRepository(EcommerceContext context)
        {
            _context = context;
        }
    }
}
