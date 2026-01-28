# Docker Deployment Guide

## Overview

This guide explains how to run the entire Movie Search application using Docker Compose, including the frontend, backend, and database.

## Architecture

The Docker setup consists of three services:

1. **postgres**: PostgreSQL 16 database
2. **backend**: .NET 8 Web API
3. **frontend**: React application served by Nginx

All services communicate through a Docker network called `moviesearch-network`.

## Prerequisites

- Docker Desktop (Windows/Mac) or Docker Engine + Docker Compose (Linux)
- At least 2GB of free RAM
- Ports 3000, 5006, and 5432 available

## Quick Start

### 1. Build and Start All Services

From the project root directory:

```bash
docker-compose up --build
```

This command will:
- Build the backend Docker image
- Build the frontend Docker image
- Pull the PostgreSQL image
- Start all three services
- Create the database and seed data

### 2. Access the Application

Once all services are running:

- **Frontend**: http://localhost:3000
- **Backend API**: http://localhost:5006
- **Database**: localhost:5432

### 3. Stop the Services

To stop all services:

```bash
docker-compose down
```

To stop and remove volumes (deletes database data):

```bash
docker-compose down -v
```

## Docker Services Configuration

### PostgreSQL (postgres)
- **Image**: postgres:16-alpine
- **Port**: 5432
- **Database**: MovieSearchDB
- **User**: postgres
- **Password**: postgres
- **Volume**: postgres_data (persistent storage)

### Backend (backend)
- **Base Image**: mcr.microsoft.com/dotnet/aspnet:8.0
- **Build Image**: mcr.microsoft.com/dotnet/sdk:8.0
- **Port**: 5006 (host) -> 8080 (container)
- **Environment**: Production
- **Connection String**: Uses postgres service hostname

### Frontend (frontend)
- **Build Image**: node:18-alpine
- **Server Image**: nginx:alpine
- **Port**: 3000 (host) -> 80 (container)
- **Proxy**: API requests forwarded to backend

## Detailed Commands

### Start Services in Detached Mode

```bash
docker-compose up -d
```

### View Logs

All services:
```bash
docker-compose logs -f
```

Specific service:
```bash
docker-compose logs -f backend
docker-compose logs -f frontend
docker-compose logs -f postgres
```

### Restart a Service

```bash
docker-compose restart backend
```

### Rebuild a Service

```bash
docker-compose up --build backend
```

### Check Service Status

```bash
docker-compose ps
```

## Environment Variables

### Backend

Set in `docker-compose.yml`:
- `ASPNETCORE_ENVIRONMENT`: Production
- `ASPNETCORE_URLS`: http://+:8080
- `ConnectionStrings__DefaultConnection`: Database connection string

### Frontend

Nginx configuration handles API proxying automatically.

## Networking

All services are connected through the `moviesearch-network` bridge network:

- Frontend -> Backend: http://backend:8080
- Backend -> Database: postgres:5432

## Volumes

### postgres_data

Persistent volume for PostgreSQL data. Survives container restarts.

To remove:
```bash
docker-compose down -v
```

## Troubleshooting

### Service Won't Start

Check logs:
```bash
docker-compose logs [service-name]
```

### Database Connection Issues

Ensure PostgreSQL is healthy:
```bash
docker-compose ps
```

Look for "healthy" status on postgres service.

### Port Conflicts

If ports are in use, modify `docker-compose.yml`:
```yaml
ports:
  - "3001:80"  # Change 3000 to 3001 for frontend
  - "5007:8080"  # Change 5006 to 5007 for backend
```

### Rebuild from Scratch

```bash
docker-compose down -v
docker-compose build --no-cache
docker-compose up
```

## Production Deployment

For production deployments:

1. **Update CORS settings** in backend to restrict origins
2. **Set strong passwords** for PostgreSQL
3. **Use environment files** for sensitive data
4. **Enable HTTPS** with SSL certificates
5. **Configure resource limits** in docker-compose.yml
6. **Set up health checks** for monitoring
7. **Use Docker secrets** for credentials

Example production docker-compose snippet:
```yaml
services:
  postgres:
    environment:
      POSTGRES_PASSWORD: ${DB_PASSWORD}
    deploy:
      resources:
        limits:
          cpus: '1'
          memory: 1G
```

## Development vs Production

### Development

Current setup is optimized for development with:
- Open CORS policy
- Development environment variables
- Exposed ports for direct access
- Debug logging enabled

### Production Recommendations

1. Use environment-specific configurations
2. Implement proper secret management
3. Add reverse proxy (e.g., Traefik, Nginx)
4. Enable SSL/TLS
5. Configure logging and monitoring
6. Set up backups for database
7. Use Docker Swarm or Kubernetes for orchestration

## Useful Commands Summary

```bash
# Build and start
docker-compose up --build

# Start in background
docker-compose up -d

# Stop services
docker-compose down

# Stop and remove volumes
docker-compose down -v

# View logs
docker-compose logs -f

# Restart service
docker-compose restart backend

# Rebuild service
docker-compose build backend

# Check status
docker-compose ps

# Remove unused images
docker image prune -a
```

## Health Checks

### PostgreSQL
```bash
docker exec moviesearch-postgres pg_isready -U postgres
```

### Backend
```bash
curl http://localhost:5006/api/movies
```

### Frontend
```bash
curl http://localhost:3000
```

## Scaling (Future)

To scale services (requires load balancer):
```bash
docker-compose up --scale backend=3
```

Note: Current setup doesn't support scaling due to port mappings. Use orchestration tools for production scaling.
