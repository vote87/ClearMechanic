namespace MovieSearch.API.DTOs;

/// <summary>
/// Request model for movie search operations.
/// </summary>
public class MovieSearchRequest
{
    public string? Title { get; set; }
    public string? Genre { get; set; }
    public string? ActorName { get; set; }
}
