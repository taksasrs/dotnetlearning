using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestAPI.Models;
using System.Transactions;
using System.Data.Entity;


namespace TestAPI.Repository
{
    public partial interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsername(string username);
        Task<bool> AddUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        public bool UserExists(string username);
    }

    public partial class UserRepository : IUserRepository
    {
        private readonly EcommerceContext _context;

        public UserRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            return user;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            //var users = from b in _context.Users
            //            where b.Username.Equals(username)
            //            select b;
            var user = await _context.Users.Where(x => x.Username == username).ToListAsync();
            ///var user = _context.Users.FromSql($"select * from [User] where username = '{username}'").ToList();

            return user.FirstOrDefault();
        }

        public async Task<bool> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            if (await _context.SaveChangesAsync() > 0)
                return true;
            else
                return false;
        }

        // public async Task<bool> UpdateMovieAsync(int id, Movie movie)
        // {
        //     if (id != movie.Id)
        //     {
        //         return false;
        //     }

        //     _context.Entry(movie).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         return false;
        //     }

        //     return true;
        // }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool UserExists(string username)
        {
            return _context.Users.Any(e => e.Username == username);
        }
    }

}
