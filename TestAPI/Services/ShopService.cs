using Microsoft.AspNetCore.Mvc;
using TestAPI.Models;
using TestAPI.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;
using StackExchange.Redis;
using DocumentFormat.OpenXml.Spreadsheet;
//using TestAPI.Repository;

namespace TestAPI.Services
{
    public partial class ShopService 
    {
        private readonly IWebRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ShopService(IWebRepository repository,IConfiguration configuration, HttpClient httpClient)
        {
            _repository = repository;
            _configuration = configuration;
            _httpClient = httpClient;
        }
        public async Task<ServiceResponse<object>> CreateShop(Shop shop)
        {
            var res = new ServiceResponse<object>();
            try
            {
                if (!_repository.ShopExists(shop.ShopId))
                {
                    res.Success = await _repository.AddShopAsync(shop);
                }
                else
                {
                    res.ErrorMessage = "Shop already exist";
                }
            }
            catch
            {
                throw;
            }
            return res;
        }

        public async Task<ServiceResponse<IActionResult>> EditShop(int id, Shop shop){
            var res = new ServiceResponse<IActionResult>();
            try
            {
                if (_repository.ShopExists(shop.ShopId))
                {
                    res.Success = await _repository.UpdateShopAsync(id, shop);
                }
                else
                {
                    res.ErrorMessage = "Shop not already exist";
                }
            }
            catch
            {
                throw;
            }
            return res;
        }

        public async Task<ServiceResponse<IActionResult>> DeleteShop(int id){
            var res = new  ServiceResponse<IActionResult>();
            try
            {
                if (_repository.ShopExists(id))
                {
                    res.Success = await _repository.DeleteShopAsync(id);
                }
                else
                {
                    res.ErrorMessage = "Shop not already exist";
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
