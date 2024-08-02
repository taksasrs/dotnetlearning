using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestAPI.Models;
using System.Transactions;
using System.Data.Entity;


namespace TestAPI.Repository
{
    public partial interface ITokenRepository
    {
        Task<bool> DeleteRefreshTokenAsync(string token);
        Task<bool> AddRefreshTokenAsync(Token token);
        bool TokenValidate(string token);
    }

    public partial class TokenRepository : ITokenRepository
    {
        private readonly EcommerceContext _context;

        public TokenRepository(EcommerceContext context)
        {
            _context = context;
        }

        //public async Task<Token> GetRefreshTokenByIdAsync(string token)
        //{
        //    var refreshToken = await _context.Tokens.FindAsync(token);

        //    return refreshToken;
        //}

        public async Task<bool> AddRefreshTokenAsync(Token token)
        {
            
            _context.Tokens.Add(token);
            if (await _context.SaveChangesAsync() > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteRefreshTokenAsync(string token)
        {
            var tok = await _context.Tokens.FindAsync(token);
            if (tok == null)
            {
                return false;
            }

            _context.Tokens.Remove(tok);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool TokenValidate(string token)
        {
            return _context.Tokens.Any(e => e.RefreshToken == token && e.RefreshTokenExpiryTime > DateTime.Now);
        }
    }

}
