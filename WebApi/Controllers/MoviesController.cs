using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly MovieService _ms;
   
    public MoviesController()
    {
        _ms = new MovieService();
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
    {
        var movies = await _ms.GetMoviesAsync();
        return Ok(movies);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovie(int id)
    {
        var movie = await _ms.GetMovieAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        return Ok(movie);
    }
    
    [HttpPost]
    public async Task<ActionResult<Movie>> Post(Movie movie)
    {
        await _ms.AddMovieAsync(movie);
        return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);        
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovie(int id, Movie movie)
    {     
        await _ms.UpdateMovieAsync(movie);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var movie = await _ms.GetMovieAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        await _ms.DeleteMovieAsync(id);
        return NoContent();
    }
}
