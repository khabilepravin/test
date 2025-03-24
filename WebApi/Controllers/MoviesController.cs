using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ILogger<MoviesController> _logger;

    public MoviesController(ISender sender, ILogger<MoviesController> logger)
    {
        _sender = sender;
        _logger = logger;
    }


    [HttpGet]
    public async Task<ActionResult<List<Movie>>> GetMovies()
    {
        
            try
            {
                var movies = await _sender.Send(new GetMoviesQuery());
                _logger.LogInformation($"Fetched movies.");
                return Ok(movies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching movies.");
                return StatusCode(500, "Internal server error.");
            }
    }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
             // TODO:: ADD PUT LOGIC
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
        // TODO:: ADD DELETE LOGIC
        return NoContent();
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovieById(int id)
        {
            try
            {
                _logger.LogInformation("Request to get movie with ID {MovieId}.", id);


                var movie = await _sender.Send(new GetMovieByIdQuery { Id = id });
                if (movie == null)
                {
                    return NotFound();
                }
                return Ok(movie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the movie with ID {MovieId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> AddMovie([FromBody] CreateMovieCommand createMovieCommand)
        {
            try
            {
                var result = await _sender.Send(createMovieCommand);
                if (result.MovieExists)
                {
                    _logger.LogWarning("Movie already exists.");
                    return Conflict("Movie already exists");
                }
                return Ok(result.AddedMovie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the movie");
                return StatusCode(500, "Internal server error");
            }
        }

}
