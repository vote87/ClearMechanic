# Backend Implementation Documentation

## Overview

The backend is implemented using .NET 8 Web API with Entity Framework Core and PostgreSQL. It provides RESTful endpoints for searching movies by title, genre, or actor name.

## Architecture

### Project Structure

```
MovieSearch.API/
├── Controllers/      # API Controllers
├── Data/            # DbContext and database configuration
├── DTOs/            # Data Transfer Objects
├── Models/          # Entity models
└── Services/        # Business logic services
```

## Implementation Steps

### Step 1: Project Setup

1. Created .NET 8 Web API project using `dotnet new webapi`
2. Added required NuGet packages:
   - `Microsoft.EntityFrameworkCore` (8.0.0)
   - `Microsoft.EntityFrameworkCore.Design` (8.0.0)
   - `Npgsql.EntityFrameworkCore.PostgreSQL` (8.0.0)

### Step 2: Database Models

Created three main entity models:

#### Movie Model
- **Properties**: Id, Title, Description, ReleaseYear, Genre
- **Relationships**: Many-to-many with Actor through MovieActor

#### Actor Model
- **Properties**: Id, Name, DateOfBirth
- **Relationships**: Many-to-many with Movie through MovieActor

#### MovieActor Model
- **Purpose**: Join table for many-to-many relationship
- **Properties**: MovieId, ActorId (composite key)

### Step 3: Database Context

Created `ApplicationDbContext` that:
- Configures Entity Framework Core with PostgreSQL
- Defines DbSets for Movies, Actors, and MovieActors
- Configures relationships in `OnModelCreating`
- Includes seed data for initial testing

**Key Configuration:**
- Many-to-many relationship configured using Fluent API
- Composite key for MovieActor join table
- Seed data includes 8 movies and 6 actors with relationships

### Step 4: Data Transfer Objects (DTOs)

Created DTOs to separate API contracts from database entities:

- **MovieDto**: Contains movie information with associated actors
- **ActorDto**: Contains actor information
- **MovieSearchRequest**: Request model for search operations

### Step 5: Service Layer

Implemented `MovieService` with the following features:

- **SearchMoviesAsync**: Searches movies based on:
  - Title (case-insensitive partial match)
  - Genre (case-insensitive partial match)
  - Actor name (case-insensitive partial match)
- Supports multiple search criteria (AND logic)
- Returns movies with their associated actors

**Search Logic:**
- Uses LINQ queries with Entity Framework Core
- Includes related entities (MovieActors and Actors) using `Include` and `ThenInclude`
- Applies filters dynamically based on provided criteria

### Step 6: API Controller

Created `MoviesController` with two endpoints:

1. **POST /api/movies/search**
   - Accepts `MovieSearchRequest` in request body
   - Validates that at least one search criteria is provided
   - Returns list of matching movies

2. **GET /api/movies**
   - Returns all movies in the database
   - Useful for testing and initial data display

### Step 7: Configuration

#### Program.cs Configuration
- Registered DbContext with PostgreSQL connection
- Configured CORS for React frontend (localhost:3000)
- Registered MovieService as scoped dependency
- Added Swagger/OpenAPI for API documentation
- Ensured database creation on startup

#### appsettings.json
- Configured PostgreSQL connection string
- Default connection: `Host=localhost;Port=5432;Database=MovieSearchDB;Username=postgres;Password=postgres`

### Step 8: Docker Configuration

Created Docker setup for PostgreSQL database:

- **docker-compose.yml**: Defines PostgreSQL service
- **Dockerfile**: For containerizing the API (optional)
- Database runs on port 5432 with persistent volume

## Database Schema

```
Movies
├── Id (PK)
├── Title
├── Description
├── ReleaseYear
└── Genre

Actors
├── Id (PK)
├── Name
└── DateOfBirth

MovieActors (Join Table)
├── MovieId (FK, PK)
└── ActorId (FK, PK)
```

## API Endpoints

### Search Movies
```
POST /api/movies/search
Content-Type: application/json

Request Body:
{
  "title": "string (optional)",
  "genre": "string (optional)",
  "actorName": "string (optional)"
}

Response: 200 OK
[
  {
    "id": 1,
    "title": "Forrest Gump",
    "description": "...",
    "releaseYear": 1994,
    "genre": "Drama",
    "actors": [...]
  }
]
```

### Get All Movies
```
GET /api/movies

Response: 200 OK
[Array of MovieDto]
```

## Seed Data

The database is seeded with:
- 8 movies across different genres
- 6 actors
- Movie-actor relationships

Sample movies include:
- Forrest Gump (Drama)
- The Revenant (Adventure)
- The Devil Wears Prada (Comedy)
- Fight Club (Drama)
- La La Land (Musical)
- Iron Man (Action)
- Cast Away (Drama)
- Inception (Sci-Fi)

## Running the Backend

1. Start PostgreSQL database:
   ```bash
   docker-compose up -d
   ```

2. Restore packages:
   ```bash
   dotnet restore
   ```

3. Run the application:
   ```bash
   dotnet run
   ```

4. API will be available at:
   - HTTP: `http://localhost:5000`
   - HTTPS: `https://localhost:5001`
   - Swagger UI: `https://localhost:5001/swagger`

## Testing

Use Swagger UI or tools like Postman to test the API endpoints. The search endpoint supports:
- Single criteria search (e.g., only title)
- Multiple criteria search (e.g., title AND genre)
- Empty search (returns all movies when using GET /api/movies)
