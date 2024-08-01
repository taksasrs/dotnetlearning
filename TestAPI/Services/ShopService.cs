﻿using Microsoft.AspNetCore.Mvc;
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
    //public interface IAuthService
    //{
    //    Task<bool> VerifyLoginAsync(string username, string password);
    //}

    public partial class WebService 
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public WebService(IUserRepository repository,IConfiguration configuration, HttpClient httpClient)
        {
            _repository = repository;
            _configuration = configuration;
            _httpClient = httpClient;
        }
        public async Task<MCommon<object>> CreateUser(User user)
        {
            var ret = new MCommon<object>();
            try
            {
                if (!_repository.UserExists(user.Username))
                {
                    ret.Success = await _repository.AddUserAsync(user);
                }
                else
                {
                    ret.ErrorMessage = "User already exist";
                }
            }
            catch
            {
                throw;
            }
            return ret;
        }
    }
}
