using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Data.Dtos.Product;
using TestAPI.Services;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        private readonly IMapper _mapper;

        public ProductController(ProductService productService, IMapper mapper){
            _productService = productService;
            _mapper = mapper;
        }

         [HttpGet]
        public async Task<IActionResult> GetAllShop(){
            var data = await _productService.GetAllProduct();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShop(CreateProductDto product){
            var data = await _productService.CreateProduct(product);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateShop(int id, UpdateProductDto product){
            var data = await _productService.EditProduct(id, product);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteShop(int id){
            var data = await _productService.DeleteProduct(id);
            return Ok(data);
        }
    }
}