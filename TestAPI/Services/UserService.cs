using Microsoft.AspNetCore.Mvc;
using TestAPI.Models;
using TestAPI.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;
using StackExchange.Redis;
using DocumentFormat.OpenXml.Spreadsheet;
using TestAPI.Data.Dtos.User;
using AutoMapper;
using System.Security.Cryptography;
using System.Text;
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
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public UserService(IUserRepository repository, IConfiguration configuration, IMapper mapper, HttpClient httpClient)
        {
            _repository = repository;
            _configuration = configuration;
            _mapper = mapper;
            _httpClient = httpClient;
        }
        public async Task<ServiceResponse<object>> CreateUser(UserDto usermap)
        {
            var ret = new ServiceResponse<object>();
            try
            {
                var hash = CreatePasswordHash(usermap.Password);
                //var user = _mapper.Map<User>(usermap);
                var user = new User
                {
                    Username = usermap.Username,
                    Password = hash.passwordHash,
                    PasswordSalt = hash.passwordSalt,
                    ChatId = usermap.ChatId,
                    CreateAt = DateTime.UtcNow,
                };

                foreach (var roleName in usermap.Roles)
                {
                    var userRole = new UserRole
                    {
                        Role = roleName,
                        Username = user.Username
                    };
                    user.Roles.Add(userRole);
                }

                if (!_repository.UserExists(user.Username))
                {
                    ret.Success = await _repository.AddUserAsync(user);
                    if (ret.Success)
                    {
                        //add roles
                        _repository.AddUserRoles(user.Roles.ToList(),user.Username);
                    }
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
        public (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA256())
            {
                var salt = hmac.Key;
                var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return (passwordHash, salt);
            }
        }

        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA256(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
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

