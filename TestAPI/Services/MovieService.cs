using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI.Models;
using TestAPI.Repository;
//using TestAPI.Repository;

namespace TestAPI.Services
{
    //public partial interface IMovieService
    //{
    //    public Task<List<Movie>> GetAllMovies();
    //}

    public partial class MovieService 
    {
        private readonly IMovieRepository _repository;

        public MovieService(IMovieRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Movie>> GetAllMovies()
        {
            //var movies = new List<Movie>();
            //movies = (List<Movie>)movies.Where(x => x.Id == 1);
            return _repository.GetAllMovies();
        }

        public Task<Movie> GetMovieByIdAsync(int id)
        {
            return _repository.GetMovieByIdAsync(id);
        }

        public Task<Movie> AddMovieAsync(Movie movie)
        {
            return _repository.AddMovieAsync(movie);
        }

        public Task<bool> UpdateMovieAsync(int id, Movie movie)
        {
            return _repository.UpdateMovieAsync(id, movie);
        }

        public Task<bool> DeleteMovieAsync(int id)
        {
            return _repository.DeleteMovieAsync(id);
        }

        public bool MovieExists(int id)
        {
            return _repository.MovieExists(id);
        }
    }
}
