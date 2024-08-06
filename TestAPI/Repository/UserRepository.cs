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
        Task<User> GetUserByUsernameAsync(string username);
        //Task<User> GetUserByUsername(string username);
        //Task<User> GetUserIdByUsername(int id);
        Task<bool> AddUserAsync(User user);
        Task<bool> DeleteUserAsync(string username);
        public bool UserExists(string username);
        Task<List<string>> GetUserRoles(string username);
    }

    public partial class UserRepository : IUserRepository
    {
        private readonly EcommerceContext _context;

        public UserRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var user = await _context.Users.FindAsync(username);

            return user;
        }
        //public async Task<User> GetUserIdByUsername(int id)
        //{
        //    var user = await _context.Users.Where(x => x.UserId == id).ToListAsync();
        //    return user.FirstOrDefault();
        //}
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

        public async Task<bool> DeleteUserAsync(string username)
        {
            var user = await _context.Users.FindAsync(username);
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

        public async Task<List<string>> GetUserRoles(string username)
        {
            var ret = new List<string>();
            var user = await _context.Roles.Where(x => x.Username == username).ToListAsync();
            foreach (var roles in user)
            {
                ret.Add(roles.Role);
            }
            return ret;
        }
    }

}
