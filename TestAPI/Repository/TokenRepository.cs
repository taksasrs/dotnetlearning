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
        void RemoveTokenExists(string username);
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
            //var tok = await _context.Tokens.Where(x => x.Username == token.Username).ToListAsync();
            //if (string.IsNullOrEmpty(tok))
            //{

            //}
            _context.Tokens.Add(token);
            if (await _context.SaveChangesAsync() > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteRefreshTokenAsync(string token)
        {
            var tok = await _context.Tokens.Where(x => x.RefreshToken == token).ToListAsync();

            //var tok = await _context.Tokens.FindAsync(token);
            if (tok == null)
            {
                return false;
            }

            _context.Tokens.Remove(tok.FirstOrDefault()!);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool TokenValidate(string token)
        {
            return _context.Tokens.Any(e => e.RefreshToken == token && e.RefreshTokenExpiryTime > DateTime.Now);
        }

        public async void RemoveTokenExists(string username)
        {
            var tok = await _context.Tokens.Where(x => x.Username == username).ToListAsync();
            foreach (var tokenExists in tok)
                _context.Tokens.Remove(tokenExists);
            await _context.SaveChangesAsync();
        }
    }

}
