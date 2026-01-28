namespace MovieSearch.API.DTOs;

/// <summary>
/// Data Transfer Object for Movie entity.
/// </summary>
public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public string Genre { get; set; } = string.Empty;
    public List<ActorDto> Actors { get; set; } = new();
}
