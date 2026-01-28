namespace MovieSearch.API.DTOs;

/// <summary>
/// Data Transfer Object for Actor entity.
/// </summary>
public class ActorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
}
