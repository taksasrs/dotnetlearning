using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestAPI.Models;
using System.Transactions;
using System.Data.Entity;


namespace TestAPI.Repositories
{
    public partial interface IMovieRepository
    {
        //Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<List<Movie>> GetAllMovies();
        Task<Movie> GetMovieByIdAsync(int id);
        Task<Movie> AddMovieAsync(Movie movie);
        Task<bool> UpdateMovieAsync(int id, Movie movie);
        Task<bool> DeleteMovieAsync(int id);
        public bool MovieExists(int id);
    }

    public partial interface IMovieRepository
    {
        void Test();
    }

    public partial class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _context;

        public MovieRepository(MovieContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            
            return await _context.Movie.ToListAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _context.Movie.FindAsync(id);
        }

        public async Task<Movie> AddMovieAsync(Movie movie)
        {
            ////using trn scope
            //using (var scope = new TransactionScope())
            //{
            //    _context.Movie.Add(movie);
            //    //use table 1
            //    await _context.SaveChangesAsync();
            //    // use table 2
            //    //3 4 5 
            //    //commit
            //    scope.Complete();
            //}

            //using trn
            using var trn = _context.Database.BeginTransaction();
            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();

            //Delete 1st movie
            //var movies = await GetMovieByIdAsync(2);
            //if (movies != null)
            //{
            //    _context.Movie.Remove(movies);
            //    await _context.SaveChangesAsync();
            //}
            trn.Commit();
            //trn.Dispose();
            return movie;

        }

        public async Task<bool> UpdateMovieAsync(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return false;
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return false;
            }

            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }

    public partial class MovieRepository : IMovieRepository
    {
        public void Test()
        {

        }
    }
}
