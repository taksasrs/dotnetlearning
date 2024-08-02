using Microsoft.AspNetCore.Mvc;
using TestAPI.Models;
using TestAPI.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;
using StackExchange.Redis;
//using TestAPI.Repository;

namespace TestAPI.Services
{
    //public interface IAuthService
    //{
    //    Task<bool> VerifyLoginAsync(string username, string password);
    //}

    public partial class AuthService 
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly RedisCacheService _redisCacheService;
        private readonly HttpClient _httpClient;

        public AuthService(IUserRepository repository,IConfiguration configuration, RedisCacheService redisCacheService, HttpClient httpClient)
        {
            _repository = repository;
            _configuration = configuration;
            _redisCacheService = redisCacheService;
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<string>> VerifyLoginAsync(string username,string password)
        {
            //verify username/pw
            var ret = new ServiceResponse<string>();
            try
            {
                var user = await _repository.GetUserByUsername(username);
                if (user == null)
                {
                    ret.ErrorMessage = "Invalid credentials";
                    return ret;
                }
                if (user.Username != null && user.Password == password)
                {
                    //valid login
                    string apilToken = _configuration["TelegramApiToken"];
                    string chatID = user.ChatId;  //7473964948
                    //generate otp and save to redis
                    var otp = await GenerateAndSaveOtpAsync(user.Username);
                    string otpstring = $"Your OTP: {otp} will expire in 1 min 30 sec";
                    string urlString = $"https://api.telegram.org/bot{apilToken}/sendMessage?chat_id={chatID}&text={otpstring}";

                    HttpResponseMessage response = await _httpClient.GetAsync(urlString);

                    // HttpClient webclient = new();
                    if (response.IsSuccessStatusCode)
                    {
                        ret.Success = true;
                    }
                }
                else
                {
                    ret.ErrorMessage = "Invalid credentials";
                }
            }
            catch
            {
                throw;
            }
            return ret;
            
        }
        public async Task<string> GenerateAndSaveOtpAsync(string key, int otpLength = 6)
        {
            string otp = GenerateOtp(otpLength);
            await _redisCacheService.SetStringAsync(key, otp, TimeSpan.FromSeconds(90));
            return otp;
        }

        public static string GenerateOtp(int length)
        {
            var random = new Random();
            var otp = new char[length];
            for (int i = 0; i < length; i++)
            {
                otp[i] = (char)('0' + random.Next(0, 10));
            }
            return new string(otp);
        }

        public async Task<bool> VerifyOtpAsync(string username, string otp)
        {
            var realOtp = await _redisCacheService.GetStringAsync(username);
            if (realOtp == otp)
                return true;
            else
                return false;
        }
    }
}
