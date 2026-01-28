namespace MovieSearch.API.DTOs;

/// <summary>
/// Enumeration of available sorting options for movies.
/// </summary>
public enum MovieOrderBy
{
    /// <summary>
    /// Sort by movie ID (ascending).
    /// </summary>
    Id = 0,

    /// <summary>
    /// Sort by movie title (alphabetical).
    /// </summary>
    Title = 1,

    /// <summary>
    /// Sort by movie genre (alphabetical).
    /// </summary>
    Genre = 2
}
