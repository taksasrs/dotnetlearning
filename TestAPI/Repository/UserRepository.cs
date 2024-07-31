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
        Task<User> GetUserByIdAsync(string username);
        Task<User> AddUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        public bool UserExists(string username);
    }

    public partial class UserRepository : IUserRepository
    {
        private readonly MovieContext _context;

        public UserRepository(MovieContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(string username)
        {
            var user = await _context.User.FindAsync(username);

            return user;
        }

        public async Task<User> AddUserAsync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
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
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool UserExists(string username)
        {
            return _context.User.Any(e => e.Username == username);
        }
    }

}
