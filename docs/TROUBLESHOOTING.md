# Troubleshooting Guide

## Error: "The LINQ expression could not be translated" when searching from Swagger

### Problem Description

When making a POST request to `/api/movies/search` from Swagger with a body like:
```json
{
  "title": "Forrest Gump"
}
```

You get this error:
```
System.InvalidOperationException: The LINQ expression 'DbSet<Movie>()
    .Where(m => m.Title.Contains(
        value: __request_Title_0, 
        comparisonType: OrdinalIgnoreCase))' could not be translated.
```

### Root Cause

The error indicates that Entity Framework Core is trying to translate `string.Contains()` with `StringComparison.OrdinalIgnoreCase` to SQL, which PostgreSQL's Npgsql provider cannot do.

### Solution

The code has been updated to:
1. **Load all movies into memory first** using `ToListAsync()`
2. **Filter in memory** using `ToLowerInvariant()` instead of `StringComparison.OrdinalIgnoreCase`

### Current Implementation

The `MovieService.SearchMoviesAsync()` method now:
- Loads all movies with their actors into memory first
- Applies filters in memory (client-side evaluation)
- Uses `ToLowerInvariant()` for case-insensitive comparison

### Why It Works from Frontend but Not Swagger

This is likely a caching issue. The frontend might be using a cached response or the backend container might have an old version of the code compiled.

### Fix Steps

1. **Stop all containers:**
   ```bash
   docker-compose down
   ```

2. **Remove Docker cache:**
   ```bash
   docker system prune -f
   ```

3. **Rebuild without cache:**
   ```bash
   docker-compose build --no-cache backend
   docker-compose up -d
   ```

4. **Wait for backend to start** (about 10 seconds)

5. **Test again from Swagger**

### Verification

The code in `backend/MovieSearch.API/Services/MovieService.cs` should:
- Line 29-32: Load all movies with `ToListAsync()` first
- Line 35: Use `IEnumerable<Movie>` for in-memory filtering
- Line 40-41: Use `ToLowerInvariant()` instead of `StringComparison`

### Alternative: Use GET Endpoint

If the POST endpoint continues to fail, you can use the GET endpoint which works correctly:
```
GET http://localhost:5006/api/movies
```

This returns all movies and you can filter on the frontend if needed.
