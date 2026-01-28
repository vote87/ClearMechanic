using Microsoft.EntityFrameworkCore;
using MovieSearch.API.Data;
using MovieSearch.API.DTOs;
using MovieSearch.API.Models;

namespace MovieSearch.API.Services;

/// <summary>
/// Service for handling movie search operations.
/// </summary>
public class MovieService : IMovieService
{
    private readonly ApplicationDbContext _context;
    
    // Maximum number of results to prevent memory issues with large datasets
    private const int MaxResults = 1000;
    
    // Maximum page size for pagination
    private const int MaxPageSize = 100;

    public MovieService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Searches for movies based on title, genre, or actor name.
    /// Uses direct projection to DTOs to avoid loading full entities into memory.
    /// </summary>
    /// <param name="request">Search criteria</param>
    /// <returns>List of matching movies (max 1000 results)</returns>
    /// <exception cref="ArgumentException">Thrown when no search criteria is provided</exception>
    public async Task<List<MovieDto>> SearchMoviesAsync(MovieSearchRequest request)
    {
        // Validate that at least one search criterion is provided
        // This prevents loading all records when no filters are applied
        if (string.IsNullOrWhiteSpace(request.Title) &&
            string.IsNullOrWhiteSpace(request.Genre) &&
            string.IsNullOrWhiteSpace(request.ActorName))
        {
            throw new ArgumentException("At least one search criterion (Title, Genre, or ActorName) must be provided.");
        }

        // Build query starting from Movies table
        // Using AsQueryable() creates a deferred query that is not executed until ToListAsync()
        var query = _context.Movies.AsQueryable();

        // Apply filters using Contains with ToLower() for case-insensitive search in SQL Server
        // SQL Server will translate this to LOWER() function calls in the generated SQL
        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            var titleLower = request.Title.Trim().ToLower();
            query = query.Where(m => m.Title.ToLower().Contains(titleLower));
        }

        if (!string.IsNullOrWhiteSpace(request.Genre))
        {
            var genreLower = request.Genre.Trim().ToLower();
            query = query.Where(m => m.Genre.ToLower().Contains(genreLower));
        }

        if (!string.IsNullOrWhiteSpace(request.ActorName))
        {
            var actorNameLower = request.ActorName.Trim().ToLower();
            // Optimized subquery to filter by actor name without loading all relationships
            // This avoids the N+1 query problem and reduces memory usage
            query = query.Where(m => 
                _context.MovieActors
                    .Where(ma => ma.MovieId == m.Id)
                    .Join(_context.Actors,
                        ma => ma.ActorId,
                        a => a.Id,
                        (ma, a) => a)
                    .Any(a => a.Name.ToLower().Contains(actorNameLower)));
        }

        // Direct projection to DTOs - only loads the data we need from the database
        // This is more efficient than loading full entities and then mapping them
        // The Select() is translated to SQL, so only required columns are fetched
        var movies = await query
            .Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                ReleaseYear = m.ReleaseYear,
                Genre = m.Genre,
                // Load actors only for the filtered movies, not all movies
                Actors = m.MovieActors
                    .Select(ma => new ActorDto
                    {
                        Id = ma.Actor.Id,
                        Name = ma.Actor.Name,
                        DateOfBirth = ma.Actor.DateOfBirth
                    })
                    .ToList()
            })
            .Take(MaxResults) // Safety limit to prevent loading millions of records
            .ToListAsync();

        return movies;
    }

    /// <summary>
    /// Gets all movies with pagination support.
    /// Uses direct projection to DTOs for optimal performance.
    /// </summary>
    /// <param name="page">Page number (1-based)</param>
    /// <param name="pageSize">Number of items per page (max 100)</param>
    /// <param name="orderBy">Sorting option (Id, Title, or Genre)</param>
    /// <returns>Paginated result of movies</returns>
    public async Task<PagedResult<MovieDto>> GetAllMoviesPagedAsync(int page, int pageSize, MovieOrderBy orderBy = MovieOrderBy.Id)
    {
        // Validate and limit page size to prevent excessive memory usage
        if (pageSize > MaxPageSize)
        {
            pageSize = MaxPageSize;
        }

        if (page < 1)
        {
            page = 1;
        }

        // Get total count for pagination metadata
        // This is a separate query but necessary for pagination information
        var totalCount = await _context.Movies.CountAsync();

        // Calculate pagination
        var skip = (page - 1) * pageSize;

        // Build base query
        var query = _context.Movies.AsQueryable();

        // Apply dynamic ordering based on the orderBy parameter
        // This is translated to SQL ORDER BY clause
        query = orderBy switch
        {
            MovieOrderBy.Title => query.OrderBy(m => m.Title),
            MovieOrderBy.Genre => query.OrderBy(m => m.Genre),
            _ => query.OrderBy(m => m.Id) // Default: order by ID
        };

        // Query with pagination and direct projection to DTOs
        // Skip() and Take() are translated to SQL OFFSET and FETCH clauses
        // Only the requested page of data is loaded into memory
        var movies = await query
            .Skip(skip)
            .Take(pageSize)
            .Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                ReleaseYear = m.ReleaseYear,
                Genre = m.Genre,
                // Load actors only for the movies in the current page
                Actors = m.MovieActors
                    .Select(ma => new ActorDto
                    {
                        Id = ma.Actor.Id,
                        Name = ma.Actor.Name,
                        DateOfBirth = ma.Actor.DateOfBirth
                    })
                    .ToList()
            })
            .ToListAsync();

        // Calculate total pages
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PagedResult<MovieDto>
        {
            Items = movies,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
            TotalPages = totalPages
        };
    }
}
