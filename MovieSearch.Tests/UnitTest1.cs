using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieSearch.API.Data;
using MovieSearch.API.DTOs;
using MovieSearch.API.Models;
using MovieSearch.API.Services;

namespace MovieSearch.Tests;

public class MovieServiceTests
{
    private static ApplicationDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task SearchMoviesAsync_WithoutCriteria_ThrowsArgumentException()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var service = new MovieService(context);
        var request = new MovieSearchRequest();

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => service.SearchMoviesAsync(request));
    }

    [Fact]
    public async Task SearchMoviesAsync_ByTitle_ReturnsMatchingMovie()
    {
        // Arrange
        using var context = CreateInMemoryContext();

        var movie = new Movie
        {
            Title = "The Matrix",
            Description = "Sci-fi movie",
            Genre = "Sci-Fi",
            ReleaseYear = 1999
        };

        context.Movies.Add(movie);
        await context.SaveChangesAsync();

        var service = new MovieService(context);
        var request = new MovieSearchRequest
        {
            Title = "matrix"
        };

        // Act
        var result = await service.SearchMoviesAsync(request);

        // Assert
        Assert.Single(result);
        Assert.Equal("The Matrix", result[0].Title);
    }

    [Fact]
    public async Task GetAllMoviesPagedAsync_ReturnsPagedResult()
    {
        // Arrange
        using var context = CreateInMemoryContext();

        for (int i = 1; i <= 15; i++)
        {
            context.Movies.Add(new Movie
            {
                Title = $"Movie {i}",
                Description = "Description",
                Genre = "Genre",
                ReleaseYear = 2000 + i
            });
        }

        await context.SaveChangesAsync();

        var service = new MovieService(context);

        // Act
        var result = await service.GetAllMoviesPagedAsync(page: 2, pageSize: 5, orderBy: MovieOrderBy.Id);

        // Assert
        Assert.Equal(5, result.Items.Count);
        Assert.Equal(15, result.TotalCount);
        Assert.Equal(2, result.Page);
        Assert.Equal(5, result.PageSize);
        Assert.Equal(3, result.TotalPages);
    }
}
