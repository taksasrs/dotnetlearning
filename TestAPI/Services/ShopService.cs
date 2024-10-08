﻿using Microsoft.AspNetCore.Mvc;
using TestAPI.Models;
using TestAPI.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;
using StackExchange.Redis;
using DocumentFormat.OpenXml.Spreadsheet;
using TestAPI.Data.Dtos.Shops;
//using TestAPI.Repository;
using AutoMapper;

namespace TestAPI.Services
{
    public partial class ShopService 
    {
        private readonly IWebRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public ShopService(IWebRepository repository,IConfiguration configuration, HttpClient httpClient, IMapper mapper)
        {
            _repository = repository;
            _configuration = configuration;
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<ShopDto> GetAllshop(int pageNumber, int pageSize){
            var data = await _repository.GetAllShop(pageNumber, pageSize);
            return data;
        }

        public async Task<Shop> GetShopById(int id){
            var data = await _repository.GetShopByIdAsync(id);
            return data;
        }     

        public async Task<ServiceResponse<object>> CreateShop(CreateShopDto shop)
        {
            var res = new ServiceResponse<object>();
            try
            {
                if(!_repository.ShopExistsByName(shop.Name)){
                    res.Success = await _repository.AddShopAsync(_mapper.Map<Shop>(shop));
                }else{
                    res.ErrorMessage = "Shop already exist";
                }
            }
            catch
            {
                throw;
            }
            return res;
        }

        public async Task<ServiceResponse<IActionResult>> EditShop(int id, UpdateShopDto shop){
            var res = new ServiceResponse<IActionResult>();
            try
            {
                if (_repository.ShopExists(shop.ShopId))
                {
                    res.Success = await _repository.UpdateShopAsync(id, _mapper.Map<Shop>(shop));
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
