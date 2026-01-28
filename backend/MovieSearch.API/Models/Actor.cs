namespace MovieSearch.API.Models;

/// <summary>
/// Represents an actor entity in the database.
/// </summary>
public class Actor
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    
    // Navigation property for many-to-many relationship with movies
    public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
}
