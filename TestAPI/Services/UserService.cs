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
    //public interface IAuthService
    //{
    //    Task<bool> VerifyLoginAsync(string username, string password);
    //}

    public partial class UserService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public UserService(IUserRepository repository, IConfiguration configuration, HttpClient httpClient)
        {
            _repository = repository;
            _configuration = configuration;
            _httpClient = httpClient;
        }
        public async Task<ServiceResponse<object>> CreateUser(User user)
        {
            var ret = new ServiceResponse<object>();
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

        public async Task<ServiceResponse<User>> GetUser(string username)
        {
            var ret = new ServiceResponse<User>();
            ret.Data = await _repository.GetUserByUsernameAsync(username);
            ret.Success = true;
            return ret;
        }

        public async Task<List<string>> GetRoles(string username)
        {
            var ret = await _repository.GetUserRoles(username);
            return ret;
        }
    }
}

