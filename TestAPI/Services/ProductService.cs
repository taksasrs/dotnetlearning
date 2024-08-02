using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Data.Dtos.Product;
using TestAPI.Models;
using TestAPI.Repository;

namespace TestAPI.Services
{
    public partial class ProductService
    {
        private readonly IWebRepository _repository;

        private readonly IMapper _mapper;

        public ProductService(IWebRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async   Task<IEnumerable<Product>> GetAllProduct(){
            var data = await _repository.GetProductAll();
            return data;
        }

        public async Task<ServiceResponse<object>> CreateProduct(CreateProductDto product)
        {
            var res = new ServiceResponse<object>();
            try
            {
                if (!_repository.ProductExistsByName(product.Name))
                {
                    res.Success = await _repository.AddProductAsync(_mapper.Map<Product>(product));
                }
                else
                {
                    res.ErrorMessage = "Product already exist";
                }
            }
            catch
            {
                throw;
            }
            return res;
        }

         public async Task<ServiceResponse<IActionResult>> EditProduct(int id, UpdateProductDto product){
            var res = new   ServiceResponse<IActionResult>();
            try
            {
                if (_repository.ProductExists(product.ProductId))
                {
                    res.Success = await _repository.UpdateProductAsync(id, _mapper.Map<Product>(product));
                }
                else
                {
                    res.ErrorMessage = "Product not already exist";
                }
            }
            catch
            {
                throw;
            }
            return res;
        }

        public async Task<ServiceResponse<IActionResult>> DeleteProduct(int id){
            var res = new   ServiceResponse<IActionResult>();
            try
            {
                if (_repository.ProductExists(id))
                {
                    res.Success = await _repository.DeleteProductAsync(id);
                }
                else
                {
                    res.ErrorMessage = "Product not already exist";
                }
            }
            catch
            {
                throw;
            }
            return res;
        }
    }
}