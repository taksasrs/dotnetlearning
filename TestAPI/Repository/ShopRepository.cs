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
        Task<Shop> GetShopByIdAsync(string shopId);
        Task<bool> AddShopAsync(Shop shop);
        //Task<bool> DeleteUserAsync(int id);
        public bool ShopExists(int shopId);
    }

    public partial class WebRepository : IWebRepository
    { 
        public async Task<Shop> GetShopByIdAsync(string shopId)
        {
            var shop = await _context.Shops.FindAsync(shopId);

            return shop;
        }

        public async Task<bool> AddShopAsync(Shop shop)
        {
            _context.Shops.Add(shop);
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

        public bool ShopExists(int shopId)
        {
            return _context.Shops.Any(e => e.ShopId == shopId);
        }
    }

}
