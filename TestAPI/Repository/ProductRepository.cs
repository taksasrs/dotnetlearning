using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestAPI.Models;
using System.Transactions;
using System.Data.Entity;


namespace TestAPI.Repository
{
    public partial interface IWebRepository
    {
        Task<Product> GetProductByIdAsync(int productId);
        Task<bool> AddProductAsync(Product prod);
        //Task<bool> DeleteUserAsync(int id);
        public bool ProductExists(int productId);
    }

    public partial class WebRepository : IWebRepository
    { 
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var prod = await _context.Products.FindAsync(productId);

            return prod;
        }

        public async Task<bool> AddProductAsync(Product prod)
        {
            _context.Products.Add(prod);
            if (await _context.SaveChangesAsync() > 0)
                return true;
            else
                return false;
        }

        // public async Task<bool> UpdateMovieAsync(int id, Movie movie)
        // {
        //     if (id != movie.Id)
        //     {
        //         return false;
        //     }

        //     _context.Entry(movie).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         return false;
        //     }

        //     return true;
        // }

        //public async Task<bool> DeleteUserAsync(int id)
        //{
        //    var user = await _context.User.FindAsync(id);
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    _context.User.Remove(user);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        public bool ProductExists(int productId)
        {
            return _context.Products.Any(e => e.ProductId == productId);
        }
    }

}
