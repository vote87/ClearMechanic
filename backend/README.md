# Backend - Movie Search API

.NET 8 Web API for movie search functionality.

## Project Structure

```
backend/
├── Controllers/      # API Controllers
├── Data/             # DbContext and database configuration
├── DTOs/             # Data Transfer Objects
├── Models/           # Entity models
├── Services/         # Business logic services
└── Migrations/       # EF Core migrations
```

## Setup Instructions

1. Install .NET 8 SDK
2. Configure database connection string in `appsettings.json`
3. Run migrations: `dotnet ef database update`
4. Run the application: `dotnet run`

## Docker Setup

See `docker-compose.yml` for database setup.
