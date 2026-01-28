namespace MovieSearch.API.Models;

/// <summary>
/// Represents a movie entity in the database.
/// </summary>
public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public string Genre { get; set; } = string.Empty;
    
    // Navigation property for many-to-many relationship with actors
    public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
}
