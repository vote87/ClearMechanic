# Setup Guide

This guide will help you set up and run the Movie Search Application.

## Prerequisites

- .NET 8 SDK
- Node.js (v18 or higher)
- Docker and Docker Compose
- PostgreSQL (or use Docker)

## Backend Setup

### Step 1: Start Database

Start PostgreSQL using Docker Compose:

```bash
docker-compose up -d
```

This will:
- Start PostgreSQL container on port 5432
- Create database `MovieSearchDB`
- Set up user `postgres` with password `postgres`

### Step 2: Configure Backend

1. Navigate to backend directory:
   ```bash
   cd backend/MovieSearch.API
   ```

2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

3. Verify connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=MovieSearchDB;Username=postgres;Password=postgres"
   }
   ```

### Step 3: Run Backend

1. Run the application:
   ```bash
   dotnet run
   ```

2. The API will be available at:
   - HTTP: `http://localhost:5000`
   - HTTPS: `https://localhost:5001`
   - Swagger UI: `https://localhost:5001/swagger`

3. The database will be automatically created and seeded with initial data on first run.

## Frontend Setup

### Step 1: Install Dependencies

1. Navigate to frontend directory:
   ```bash
   cd frontend
   ```

2. Install npm packages:
   ```bash
   npm install
   ```

### Step 2: Configure Environment (Optional)

Create `.env` file in frontend directory:

```
VITE_API_URL=http://localhost:5000/api
```

If not provided, the default URL `http://localhost:5000/api` will be used.

### Step 3: Run Frontend

1. Start development server:
   ```bash
   npm run dev
   ```

2. The application will be available at `http://localhost:3000`

## Running Both Services

### Option 1: Separate Terminals

1. Terminal 1 - Database:
   ```bash
   docker-compose up -d
   ```

2. Terminal 2 - Backend:
   ```bash
   cd backend/MovieSearch.API
   dotnet run
   ```

3. Terminal 3 - Frontend:
   ```bash
   cd frontend
   npm run dev
   ```

### Option 2: Using Scripts (Windows PowerShell)

Create a script to run all services:

```powershell
# Start database
docker-compose up -d

# Start backend (in new window)
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd backend/MovieSearch.API; dotnet run"

# Start frontend (in new window)
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd frontend; npm run dev"
```

## Verification

1. **Database**: Check if PostgreSQL is running:
   ```bash
   docker ps
   ```

2. **Backend**: Open Swagger UI at `https://localhost:5001/swagger` and test the endpoints.

3. **Frontend**: Open `http://localhost:3000` and try searching for movies.

## Troubleshooting

### Database Connection Issues

- Ensure Docker is running
- Check if PostgreSQL container is up: `docker ps`
- Verify connection string in `appsettings.json`
- Check PostgreSQL logs: `docker logs moviesearch-postgres`

### Backend Issues

- Ensure .NET 8 SDK is installed: `dotnet --version`
- Restore packages: `dotnet restore`
- Check if port 5000/5001 is available
- Review application logs in console

### Frontend Issues

- Ensure Node.js is installed: `node --version`
- Clear node_modules and reinstall: `rm -rf node_modules && npm install`
- Check if port 3000 is available
- Verify API URL in browser console

### CORS Issues

If you see CORS errors:
- Verify CORS configuration in `Program.cs`
- Ensure frontend URL matches CORS policy (http://localhost:3000)
- Check browser console for specific error messages

## Production Build

### Backend

```bash
cd backend/MovieSearch.API
dotnet publish -c Release -o ./publish
```

### Frontend

```bash
cd frontend
npm run build
```

Output will be in `frontend/dist/` directory.

## Database Migrations

If you need to create migrations manually:

```bash
cd backend/MovieSearch.API
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Note: The application uses `EnsureCreated()` for simplicity, but migrations are recommended for production.

## Stopping Services

1. Stop frontend: `Ctrl+C` in frontend terminal
2. Stop backend: `Ctrl+C` in backend terminal
3. Stop database: `docker-compose down`

To remove database volume (deletes all data):
```bash
docker-compose down -v
```
