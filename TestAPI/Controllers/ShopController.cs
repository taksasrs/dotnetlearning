using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Data.Dtos;
using TestAPI.Models;
using TestAPI.Services;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopController : ControllerBase
    {
        private readonly ShopService _shopService;

        public ShopController(ShopService shopService){
            _shopService = shopService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShop(){
            var data = await _shopService.GetAllshop();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShop(CreateShopDto shop){
            var data = await _shopService.CreateShop(shop);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateShop(int id, UpdateShopDto shop){
            var data = await _shopService.EditShop(id, shop);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteShop(int id){
            var data = await _shopService.DeleteShop(id);
            return Ok(data);
        }
    }
}