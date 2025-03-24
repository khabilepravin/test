using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services;

public class MovieService
{
    private readonly List<Movie> _movies = new();
    private int _nextId = 1;

    public async Task<IEnumerable<Movie>> GetMoviesAsync()
    {
        return await Task.FromResult(_movies);
    }

    public async Task<Movie> GetMovieAsync(int id)
    {
        return await Task.FromResult(_movies.FirstOrDefault(m => m.Id == id));
    }

    public async Task AddMovieAsync(Movie movie)
    {
        movie.Id = _nextId++;
        _movies.Add(movie);
        await Task.CompletedTask;
    }

    public async Task UpdateMovieAsync(Movie movie)
    {
        var existingMovie = _movies.FirstOrDefault(m => m.Id == movie.Id);
        if (existingMovie != null)
        {
            _movies.Remove(existingMovie);
            _movies.Add(movie);
        }
        await Task.CompletedTask;
    }

    public async Task DeleteMovieAsync(int id)
    {
        var movie = _movies.FirstOrDefault(m => m.Id == id);
        if (movie != null)
        {
            _movies.Remove(movie);
        }
        await Task.CompletedTask;
    }
}
