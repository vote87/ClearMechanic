using MovieSearch.API.DTOs;

namespace MovieSearch.API.Services;

/// <summary>
/// Interface for movie search service operations.
/// </summary>
public interface IMovieService
{
    /// <summary>
    /// Searches for movies based on search criteria.
    /// Requires at least one search criterion to be provided.
    /// </summary>
    /// <param name="request">Search criteria</param>
    /// <returns>List of matching movies (max 1000 results)</returns>
    Task<List<MovieDto>> SearchMoviesAsync(MovieSearchRequest request);

    /// <summary>
    /// Gets all movies with pagination support.
    /// </summary>
    /// <param name="page">Page number (1-based)</param>
    /// <param name="pageSize">Number of items per page (max 100)</param>
    /// <param name="orderBy">Sorting option (Id, Title, or Genre)</param>
    /// <returns>Paginated result of movies</returns>
    Task<PagedResult<MovieDto>> GetAllMoviesPagedAsync(int page, int pageSize, MovieOrderBy orderBy = MovieOrderBy.Id);
}
