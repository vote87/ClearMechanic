using Microsoft.EntityFrameworkCore;
using MovieSearch.API.Models;

namespace MovieSearch.API.Data;

/// <summary>
/// Database context for the Movie Search application.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<MovieActor> MovieActors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure many-to-many relationship between Movie and Actor
        modelBuilder.Entity<MovieActor>()
            .HasKey(ma => new { ma.MovieId, ma.ActorId });

        modelBuilder.Entity<MovieActor>()
            .HasOne(ma => ma.Movie)
            .WithMany(m => m.MovieActors)
            .HasForeignKey(ma => ma.MovieId);

        modelBuilder.Entity<MovieActor>()
            .HasOne(ma => ma.Actor)
            .WithMany(a => a.MovieActors)
            .HasForeignKey(ma => ma.ActorId);

        // Seed initial data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Actors
        var actors = new List<Actor>
        {
            new Actor { Id = 1, Name = "Tom Hanks", DateOfBirth = DateTime.SpecifyKind(new DateTime(1956, 7, 9), DateTimeKind.Utc) },
            new Actor { Id = 2, Name = "Leonardo DiCaprio", DateOfBirth = DateTime.SpecifyKind(new DateTime(1974, 11, 11), DateTimeKind.Utc) },
            new Actor { Id = 3, Name = "Meryl Streep", DateOfBirth = DateTime.SpecifyKind(new DateTime(1949, 6, 22), DateTimeKind.Utc) },
            new Actor { Id = 4, Name = "Brad Pitt", DateOfBirth = DateTime.SpecifyKind(new DateTime(1963, 12, 18), DateTimeKind.Utc) },
            new Actor { Id = 5, Name = "Emma Stone", DateOfBirth = DateTime.SpecifyKind(new DateTime(1988, 11, 6), DateTimeKind.Utc) },
            new Actor { Id = 6, Name = "Robert Downey Jr.", DateOfBirth = DateTime.SpecifyKind(new DateTime(1965, 4, 4), DateTimeKind.Utc) },
            new Actor { Id = 7, Name = "Robin Wright", DateOfBirth = DateTime.SpecifyKind(new DateTime(1966, 4, 8), DateTimeKind.Utc) },
            new Actor { Id = 8, Name = "Gary Sinise", DateOfBirth = DateTime.SpecifyKind(new DateTime(1955, 3, 17), DateTimeKind.Utc) },
            new Actor { Id = 9, Name = "Tom Hardy", DateOfBirth = DateTime.SpecifyKind(new DateTime(1977, 9, 15), DateTimeKind.Utc) },
            new Actor { Id = 10, Name = "Anne Hathaway", DateOfBirth = DateTime.SpecifyKind(new DateTime(1982, 11, 12), DateTimeKind.Utc) },
            new Actor { Id = 11, Name = "Ryan Gosling", DateOfBirth = DateTime.SpecifyKind(new DateTime(1980, 11, 12), DateTimeKind.Utc) },
            new Actor { Id = 12, Name = "Edward Norton", DateOfBirth = DateTime.SpecifyKind(new DateTime(1969, 8, 18), DateTimeKind.Utc) },
            new Actor { Id = 13, Name = "Gwyneth Paltrow", DateOfBirth = DateTime.SpecifyKind(new DateTime(1972, 9, 27), DateTimeKind.Utc) },
            new Actor { Id = 14, Name = "Jeff Bridges", DateOfBirth = DateTime.SpecifyKind(new DateTime(1949, 12, 4), DateTimeKind.Utc) },
            new Actor { Id = 15, Name = "Marion Cotillard", DateOfBirth = DateTime.SpecifyKind(new DateTime(1975, 9, 30), DateTimeKind.Utc) },
            new Actor { Id = 16, Name = "Helen Hunt", DateOfBirth = DateTime.SpecifyKind(new DateTime(1963, 6, 15), DateTimeKind.Utc) },
            new Actor { Id = 17, Name = "Ellen Page", DateOfBirth = DateTime.SpecifyKind(new DateTime(1987, 2, 21), DateTimeKind.Utc) },
            new Actor { Id = 18, Name = "Emily Blunt", DateOfBirth = DateTime.SpecifyKind(new DateTime(1983, 2, 23), DateTimeKind.Utc) },
        };

        modelBuilder.Entity<Actor>().HasData(actors);

        // Seed Movies
        var movies = new List<Movie>
        {
            new Movie { Id = 1, Title = "Forrest Gump", Description = "The presidencies of Kennedy and Johnson, the events of Vietnam, Watergate, and other historical events unfold from the perspective of an Alabama man with an IQ of 75.", ReleaseYear = 1994, Genre = "Drama" },
            new Movie { Id = 2, Title = "The Revenant", Description = "A frontiersman on a fur trading expedition in the 1820s fights for survival after being mauled by a bear.", ReleaseYear = 2015, Genre = "Adventure" },
            new Movie { Id = 3, Title = "The Devil Wears Prada", Description = "A smart but sensible new graduate lands a job as an assistant to Miranda Priestly, the demanding editor-in-chief of a high-fashion magazine.", ReleaseYear = 2006, Genre = "Comedy" },
            new Movie { Id = 4, Title = "Fight Club", Description = "An insomniac office worker and a devil-may-care soapmaker form an underground fight club that evolves into something much bigger.", ReleaseYear = 1999, Genre = "Drama" },
            new Movie { Id = 5, Title = "La La Land", Description = "While navigating their careers in Los Angeles, a pianist and an actress fall in love while attempting to reconcile their aspirations for the future.", ReleaseYear = 2016, Genre = "Musical" },
            new Movie { Id = 6, Title = "Iron Man", Description = "After being held captive in an Afghan cave, billionaire engineer Tony Stark creates a unique weaponized suit of armor to fight evil.", ReleaseYear = 2008, Genre = "Action" },
            new Movie { Id = 7, Title = "Cast Away", Description = "A FedEx executive undergoes a physical and emotional transformation after crash landing on a deserted island.", ReleaseYear = 2000, Genre = "Drama" },
            new Movie { Id = 8, Title = "Inception", Description = "A thief who steals corporate secrets through dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.", ReleaseYear = 2010, Genre = "Sci-Fi" },
        };

        modelBuilder.Entity<Movie>().HasData(movies);

        // Seed MovieActor relationships (2-4 actors per movie)
        var movieActors = new List<MovieActor>
        {
            // Forrest Gump (3 actors)
            new MovieActor { MovieId = 1, ActorId = 1 }, // Tom Hanks
            new MovieActor { MovieId = 1, ActorId = 7 }, // Robin Wright
            new MovieActor { MovieId = 1, ActorId = 8 }, // Gary Sinise
            
            // The Revenant (3 actors)
            new MovieActor { MovieId = 2, ActorId = 2 }, // Leonardo DiCaprio
            new MovieActor { MovieId = 2, ActorId = 9 }, // Tom Hardy
            new MovieActor { MovieId = 2, ActorId = 4 }, // Brad Pitt
            
            // The Devil Wears Prada (4 actors)
            new MovieActor { MovieId = 3, ActorId = 3 }, // Meryl Streep
            new MovieActor { MovieId = 3, ActorId = 10 }, // Anne Hathaway
            new MovieActor { MovieId = 3, ActorId = 18 }, // Emily Blunt
            new MovieActor { MovieId = 3, ActorId = 13 }, // Gwyneth Paltrow
            
            // Fight Club (3 actors)
            new MovieActor { MovieId = 4, ActorId = 4 }, // Brad Pitt
            new MovieActor { MovieId = 4, ActorId = 12 }, // Edward Norton
            new MovieActor { MovieId = 4, ActorId = 13 }, // Gwyneth Paltrow
            
            // La La Land (2 actors)
            new MovieActor { MovieId = 5, ActorId = 5 }, // Emma Stone
            new MovieActor { MovieId = 5, ActorId = 11 }, // Ryan Gosling
            
            // Iron Man (4 actors)
            new MovieActor { MovieId = 6, ActorId = 6 }, // Robert Downey Jr.
            new MovieActor { MovieId = 6, ActorId = 13 }, // Gwyneth Paltrow
            new MovieActor { MovieId = 6, ActorId = 14 }, // Jeff Bridges
            new MovieActor { MovieId = 6, ActorId = 18 }, // Emily Blunt
            
            // Cast Away (2 actors)
            new MovieActor { MovieId = 7, ActorId = 1 }, // Tom Hanks
            new MovieActor { MovieId = 7, ActorId = 16 }, // Helen Hunt
            
            // Inception (4 actors)
            new MovieActor { MovieId = 8, ActorId = 2 }, // Leonardo DiCaprio
            new MovieActor { MovieId = 8, ActorId = 9 }, // Tom Hardy
            new MovieActor { MovieId = 8, ActorId = 15 }, // Marion Cotillard
            new MovieActor { MovieId = 8, ActorId = 17 }, // Ellen Page
        };

        modelBuilder.Entity<MovieActor>().HasData(movieActors);
    }
}
