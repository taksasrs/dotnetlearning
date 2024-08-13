using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestAPI.Models;
using System.Transactions;
using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Helpers;
using TestAPI.Data.Dtos.Products;
using AutoMapper;


namespace TestAPI.Repository
{
    public partial interface IWebRepository
    {
        Task<ProductDto> GetProductAll(int pageNumber, int pageSize);
        Task<Product> GetProductByIdAsync(int productId);
        Task<ProductDto> GetProductsByShopId(int shopId, int pageNumber, int pageSize);
        Task<bool> AddProductAsync(Product prod);
        Task<bool> UpdateProductAsync(int id, Product prod);
        Task<bool> DeleteProductAsync(int id);
        public bool ProductExists(int productId);
        public bool ProductExistsByName(string name);
    }

    public partial class WebRepository : IWebRepository
    {

        // private readonly IMapper _mapper;

        // public WebRepository(IMapper mapper)
        // {
        //     _mapper = mapper;
        // }

        public async Task<ProductDto> GetProductAll(int pageNumber, int pageSize)
        {
            var data = await _context.Products.ToListAsync();
            var paginatedData = new PaginatedList<Product>(data, pageNumber, pageSize);


            var productDto = new ProductDto();
            productDto.content = paginatedData;
            productDto.pageable = paginatedData.paginateDto;

            return productDto;
        }
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var prod = await _context.Products.FindAsync(productId);

            return prod;
        }
        public async Task<ProductDto> GetProductsByShopId(int shopId, int pageNumber, int pageSize)
        {
            var prod = await _context.Products.ToListAsync();

            // var data = prod.Where( p => p.ShopId == shopId);
            var data = _mapper.Map<List<Product>>(prod.Where(p => p.ShopId == shopId));
            // var data = (List<Product>) prod.Where(p => p.ShopId == shopId);

            var paginatedData = new PaginatedList<Product>(data, pageNumber, pageSize);

            var productDto = new ProductDto();
            productDto.content = paginatedData;
            productDto.pageable = paginatedData.paginateDto;

            return productDto;
        }
        public bool ProductExists(int productId)
        {
            return _context.Products.Any(e => e.ProductId == productId);
        }

        public bool ProductExistsByName(string name)
        {
            var data = _context.Products.FirstOrDefault(p => p.Name.Equals(name));
            if (data != null)
            {
                return true;
            }
            else
            {
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
                if (id != product.ProductId)
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
                var shop = await _context.Products.FindAsync(id);

                if (shop == null)
                {
                    transaction.Rollback();
                    return false;
                }

                _context.Products.Remove(shop);
                await _context.SaveChangesAsync();
                transaction.Commit();
                return true;
            }

        }
    }

}
