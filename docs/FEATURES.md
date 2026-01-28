# Features Documentation

## Functional Requirements Implementation

### 1. Movie Search by Multiple Criteria

**Requirement**: Search movies by title, genre, or actor name.

**Implementation**:
- **Backend**: `MovieService.SearchMoviesAsync()` method supports searching by:
  - Title (case-insensitive partial match)
  - Genre (case-insensitive partial match)
  - Actor name (case-insensitive partial match)
- **Frontend**: `SearchForm` component provides three input fields for search criteria
- **API Endpoint**: `POST /api/movies/search` accepts `MovieSearchRequest` with optional fields

**Usage Example**:
```json
{
  "title": "Forrest",
  "genre": "Drama",
  "actorName": "Tom"
}
```

### 2. Movie-Actor Relationship

**Requirement**: Each movie must be associated with one or more actors.

**Implementation**:
- **Database**: Many-to-many relationship between `Movie` and `Actor` entities
- **Join Table**: `MovieActor` entity manages the relationship
- **Data Model**: Movies include their associated actors in the response
- **Seed Data**: All movies in the database have at least one associated actor

### 3. Search Results Display

**Requirement**: Results must be displayed on the same page below the search form.

**Implementation**:
- **Component Structure**: `App` component contains both `SearchForm` and `MovieList`
- **State Management**: Redux store manages search results globally
- **Real-time Updates**: Results update immediately after search submission
- **Responsive Layout**: Grid layout adapts to screen size (1 column mobile, 3 columns desktop)

## Technical Requirements Implementation

### Backend Technologies

#### .NET 8
- ✅ Latest .NET 8 framework used
- ✅ Web API project template
- ✅ Modern C# features (nullable reference types, implicit usings)

#### Database
- ✅ PostgreSQL database configured
- ✅ Docker Compose setup for easy database deployment
- ✅ Connection string configuration in `appsettings.json`

#### Entity Framework Core
- ✅ EF Core 8.0 used as ORM
- ✅ Code-first approach with DbContext
- ✅ Fluent API for relationship configuration
- ✅ Seed data included in DbContext

#### Docker
- ✅ Docker Compose file for PostgreSQL
- ✅ Dockerfile for API containerization (optional)
- ✅ Health checks configured

### Frontend Technologies

#### React with TypeScript
- ✅ React 18 with functional components
- ✅ TypeScript for type safety
- ✅ No class components (all functional)

#### Vite
- ✅ Vite as build tool and dev server
- ✅ Fast HMR (Hot Module Replacement)
- ✅ Optimized production builds

#### Tailwind CSS
- ✅ Tailwind CSS 3.4 configured
- ✅ Utility-first CSS approach
- ✅ Responsive design utilities
- ✅ Custom configuration available

#### Material-UI (MUI)
- ✅ MUI 5.15 for component library
- ✅ Consistent design system
- ✅ Accessible components
- ✅ Icons library included

#### Redux
- ✅ Redux Toolkit for state management
- ✅ Async thunks for API calls
- ✅ Typed hooks (useSelector, useDispatch)
- ✅ Centralized state management

## Additional Features

### Error Handling
- Backend validation for search requests
- Frontend error display in UI
- Loading states during API calls
- Empty state messages

### User Experience
- Responsive design (mobile, tablet, desktop)
- Loading indicators
- Hover effects on cards
- Clear button to reset search
- Form validation

### Code Quality
- TypeScript for type safety
- Separation of concerns (services, store, components)
- Reusable components
- Comprehensive documentation
- English comments throughout codebase

## API Features

### Endpoints

1. **POST /api/movies/search**
   - Search movies by criteria
   - Returns array of MovieDto with actors
   - Validates input

2. **GET /api/movies**
   - Get all movies
   - Useful for testing

### Response Format

```json
[
  {
    "id": 1,
    "title": "Forrest Gump",
    "description": "...",
    "releaseYear": 1994,
    "genre": "Drama",
    "actors": [
      {
        "id": 1,
        "name": "Tom Hanks",
        "dateOfBirth": "1956-07-09T00:00:00"
      }
    ]
  }
]
```

## Search Capabilities

### Single Criteria Search
- Search by title only
- Search by genre only
- Search by actor name only

### Multiple Criteria Search
- Combine title and genre
- Combine title and actor name
- Combine genre and actor name
- Combine all three criteria (AND logic)

### Search Behavior
- Case-insensitive matching
- Partial string matching (contains)
- Returns all movies matching ALL provided criteria

## Data Model

### Movies
- 8 sample movies across different genres
- Each movie has title, description, release year, and genre
- Each movie associated with at least one actor

### Actors
- 6 sample actors
- Each actor has name and date of birth
- Actors can be in multiple movies

### Genres
- Drama
- Adventure
- Comedy
- Musical
- Action
- Sci-Fi

## Future Enhancement Opportunities

While not required, these could be added:
- Unit tests (backend and frontend)
- Integration tests
- E2E tests
- Pagination for large result sets
- Advanced filtering (date ranges, multiple genres)
- Movie detail page
- Actor detail page
- User authentication
- Favorites/bookmarks
- Search history
- Performance optimizations (caching, indexing)
