using Microsoft.AspNetCore.Mvc;
using MovieSearch.API.DTOs;
using MovieSearch.API.Services;

namespace MovieSearch.API.Controllers;

/// <summary>
/// Controller for movie search operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    /// <summary>
    /// Searches for movies by title, genre, or actor name.
    /// At least one search criterion must be provided.
    /// </summary>
    /// <param name="request">Search criteria</param>
    /// <returns>List of matching movies (max 1000 results)</returns>
    [HttpPost("search")]
    public async Task<ActionResult<List<MovieDto>>> SearchMovies([FromBody] MovieSearchRequest request)
    {
        if (request == null)
        {
            return BadRequest("Search request cannot be null. Please provide at least one search criterion.");
        }

        try
        {
            var movies = await _movieService.SearchMoviesAsync(request);
            return Ok(movies);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Gets all movies with pagination support.
    /// Use this endpoint instead of search without criteria to avoid loading all records.
    /// </summary>
    /// <param name="page">Page number (default: 1)</param>
    /// <param name="pageSize">Number of items per page (default: 50, max: 100)</param>
    /// <param name="orderBy">Sorting option: 0=Id, 1=Title, 2=Genre (default: 0=Id)</param>
    /// <returns>Paginated result of movies</returns>
    [HttpGet]
    public async Task<ActionResult<PagedResult<MovieDto>>> GetAllMovies(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        [FromQuery] MovieOrderBy orderBy = MovieOrderBy.Id)
    {
        var result = await _movieService.GetAllMoviesPagedAsync(page, pageSize, orderBy);
        return Ok(result);
    }
}
