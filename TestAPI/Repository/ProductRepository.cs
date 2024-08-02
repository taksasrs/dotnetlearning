using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestAPI.Models;
using System.Transactions;
using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;


namespace TestAPI.Repository
{
    public partial interface IWebRepository
    {
        Task<IEnumerable<Product>> GetProductAll();
        Task<Product> GetProductByIdAsync(int productId);
        Task<bool> AddProductAsync(Product prod);
        Task<bool> UpdateProductAsync(int id, Product prod);
        Task<bool> DeleteProductAsync(int id);
        public bool ProductExists(int productId);

        public bool ProductExistsByName(string name);
    }

    public partial class WebRepository : IWebRepository
    {

        public async Task<IEnumerable<Product>>GetProductAll(){
            var data = await _context.Products.ToListAsync();
            return data;
        }
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var prod = await _context.Products.FindAsync(productId);

            return prod;
        }
        public bool ProductExists(int productId)
        {
            return _context.Products.Any(e => e.ProductId == productId);
        }

        public bool ProductExistsByName(string name){
            var data = _context.Products.FirstOrDefault(p => p.Name.Equals(name));
            if(data != null){
                return true;
            }else{
                return false;
            }
        }
         public async Task<bool> AddProductAsync(Product product)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Products.Add(product);
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

        public async Task<bool> UpdateProductAsync(int id, Product product)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                if (id != product.ShopId)
                {
                    transaction.Rollback();
                    return false;
                }

                _context.Entry(product).State = EntityState.Modified;

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

        public async Task<bool> DeleteProductAsync(int id)
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
    }

}
