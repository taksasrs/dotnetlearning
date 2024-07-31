using System;
using System.Collections.Generic;
using System.Linq;
using TestAPI.Controllers;
using TestAPI.Services;
using TestAPI.Repositories;
using TestAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TestAPI.Test;

public class UnitTest1 : DbContext
{
    private readonly MovieContext _context;
    private readonly MoviesController _controller;
    private readonly MovieService _service;
    private readonly MovieRepository _repo;

    public UnitTest1()
    {
        var options = new DbContextOptionsBuilder<MovieContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new MovieContext(options);
        _repo = new MovieRepository(_context);
        _service = new MovieService(_repo);
        _controller = new MoviesController(_service);

        //SeedDatabase();
    }

    private void SeedDatabase()
    {
        var movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Movie 1", Genre = "Action", ReleaseDate = DateTime.Now.AddYears(-1) },
                new Movie { Id = 2, Title = "Movie 2", Genre = "Drama", ReleaseDate = DateTime.Now.AddYears(-2) }
            };

        _context.Movie.AddRange(movies);
        _context.SaveChanges();
    }
    //[Fact]
    //public void Test1()
    //{

    //}
    [Fact]
    public void GetMovies_ReturnsAllMovies()
    {
        // Act
        var result = _service.GetAllMovies();

        // Assert
        var movies = Assert.IsType<List<Movie>>(result);
        Assert.Equal(movies, result);
        // Act
        //var result = _controller.GetMovies();

        //Assert
        //var movies = Assert.IsType<List<Movie>>(result);
        //Assert.Equal(2, movies.Count);
    }
    [Theory]
    [InlineData(1)]
    public void GetMovies_GetById(int id)
    {
        //Act
        var result = _service.GetMovieByIdAsync(id);

        //Assert
        var movie = Assert.IsType<Task<Movie>>(result);
        Assert.Equal(movie, result);
      
    }

    [Fact]
    public void InsertMovie()
    {
        Movie movie = new();
        var res = _service.AddMovieAsync(movie);
        //Act
        //var actual = typeof(Task<Movie>);
        var actual = Assert.IsType<Task<Movie>>(res);
        Assert.Equal(res, actual);
    }
}
