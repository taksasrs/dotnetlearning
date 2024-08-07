using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestAPI.Models;
using System.Transactions;
using System.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using TestAPI.Helpers;
using TestAPI.Data.Dtos.Shops;

namespace TestAPI.Repository
{
    public partial interface IWebRepository
    {
        Task<ShopDto>  GetAllShop(int pageNumber, int pageSize);
        Task<Shop> GetShopByIdAsync(int shopId);
        Task<bool> AddShopAsync(Shop shop);
        Task<bool> UpdateShopAsync(int id, Shop shop);
        Task<bool> DeleteShopAsync(int id);
        public bool ShopExists(int shopId);

        public bool ShopExistsByName(string name);
    }

    public partial class WebRepository : IWebRepository
    {

        public async Task<ShopDto> GetAllShop(int pageNumber, int pageSize)
        {
            var data = await _context.Shops.ToListAsync();
            var paginatedData = new PaginatedList<Shop>(data, pageNumber, pageSize);

            // int totalPages = paginatedData.TotalPages;

            var shopDto = new ShopDto();
            shopDto.content = paginatedData;
            shopDto.pageable = paginatedData.paginateDto;

            return shopDto;
        }
        public async Task<Shop> GetShopByIdAsync(int shopId)
        {
            var shop = await _context.Shops.FindAsync(shopId);
            return shop;
        }

        public async Task<bool> AddShopAsync(Shop shop)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Shops.Add(shop);
                if (await _context.SaveChangesAsync() > 0)
                {
                    transaction.Commit();
                    return true;
                }
                else
                    transaction.Rollback();
                return false;
            }
        }

        public async Task<bool> UpdateShopAsync(int id, Shop shop)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                if (id != shop.ShopId)
                {
                    transaction.Rollback();
                    return false;
                }

                _context.Entry(shop).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                    return true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> DeleteShopAsync(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var shop = await _context.Shops.FindAsync(id);

                if (shop == null)
                {
                    transaction.Rollback();
                    return false;
                }

                _context.Shops.Remove(shop);
                await _context.SaveChangesAsync();
                transaction.Commit();
                return true;
            }
        }

        public bool ShopExists(int shopId)
        {
            return _context.Shops.Any(e => e.ShopId == shopId);
        }

        public bool ShopExistsByName(string name)
        {
            var data = _context.Shops.FirstOrDefault(s => s.Name.Equals(name));
            if (data != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
