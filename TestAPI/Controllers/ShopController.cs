using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Data.Dtos.Shops;
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
        public async Task<IActionResult> GetAllShop(int pageNumber = 1, int pageSize = 10){
            var data = await _shopService.GetAllshop(pageNumber, pageSize);
            if(data != null){
                return Ok(data);
            }else{
                return NotFound(data);
            }
        }

       
        [HttpGet("{id}")]
        // [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetShopById(int id){
            var data = await _shopService.GetShopById(id);
            if(data != null){
                return Ok(data);
            }else{
                 return NotFound(data);
            }
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateShop(CreateShopDto shop){
            var data = await _shopService.CreateShop(shop);
            if(data.Success == true){
                return Ok(data);
            }else{
                return BadRequest(data);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateShop(int id, UpdateShopDto shop){
            var data = await _shopService.EditShop(id, shop);
            if(data.Success == true){
                return Ok(data);
            }else{
                return BadRequest(data);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteShop(int id){
            var data = await _shopService.DeleteShop(id);
            if(data.Success == true){
                return Ok(data);
            }else{
                return BadRequest(data);
            }
        }
    }
}