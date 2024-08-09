using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Data.Dtos.Products;
using TestAPI.Services;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        private readonly IMapper _mapper;

        public ProductController(ProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllProduct(int pageNumber = 1, int pageSize = 10)
        {
            var data = await _productService.GetAllProduct(pageNumber, pageSize);
            return Ok(data);
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var data = await _productService.GetProductById(id);
            return Ok(data);
        }

        [HttpGet("{shopId}")]
        public async Task<IActionResult> GetAllProductByShopId(int shopId, int pageNumber = 1, int pageSize = 10)
        {
            var data = await _productService.GetProductsByShopId(shopId, pageNumber, pageSize);
            return Ok(data);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateShop(CreateProductDto product)
        {
            var data = await _productService.CreateProduct(product);
            return Ok(data);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateShop(int id, UpdateProductDto product)
        {
            var data = await _productService.EditProduct(id, product);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteShop(int id)
        {
            var data = await _productService.DeleteProduct(id);
            return Ok(data);
        }
    }
}