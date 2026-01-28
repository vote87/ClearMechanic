namespace MovieSearch.API.Models;

/// <summary>
/// Represents the join table for the many-to-many relationship between Movies and Actors.
/// </summary>
public class MovieActor
{
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
    
    public int ActorId { get; set; }
    public Actor Actor { get; set; } = null!;
}
