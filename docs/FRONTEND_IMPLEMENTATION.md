# Frontend Implementation Documentation

## Overview

The frontend is implemented using React 18 with TypeScript, Vite as the build tool, Tailwind CSS for styling, Material-UI for components, and Redux Toolkit for state management.

## Architecture

### Project Structure

```
frontend/
├── src/
│   ├── components/    # React components
│   │   ├── SearchForm.tsx
│   │   ├── MovieList.tsx
│   │   └── MovieCard.tsx
│   ├── pages/         # Page components (future use)
│   ├── store/         # Redux store configuration
│   │   ├── store.ts
│   │   └── movieSlice.ts
│   ├── services/      # API service calls
│   │   └── movieService.ts
│   ├── types/         # TypeScript type definitions
│   │   └── movie.ts
│   ├── utils/         # Utility functions
│   ├── App.tsx        # Main application component
│   ├── main.tsx       # Application entry point
│   └── index.css      # Global styles
```

## Implementation Steps

### Step 1: Project Setup

1. Created Vite project with React TypeScript template
2. Installed dependencies:
   - **React & React DOM** (18.2.0)
   - **Redux Toolkit** (2.0.1) - State management
   - **React Redux** (9.0.4) - React bindings for Redux
   - **Material-UI** (5.15.0) - Component library
   - **Axios** (1.6.5) - HTTP client
   - **Tailwind CSS** (3.4.0) - Utility-first CSS framework

3. Configured build tools:
   - **Vite**: Fast build tool and dev server
   - **TypeScript**: Type safety
   - **PostCSS & Autoprefixer**: CSS processing

### Step 2: Type Definitions

Created TypeScript interfaces in `src/types/movie.ts`:

- **Movie**: Complete movie information with actors
- **Actor**: Actor information
- **MovieSearchRequest**: Search criteria structure

These types ensure type safety across the application and match the backend DTOs.

### Step 3: API Service Layer

Created `movieService.ts` that:

- Uses Axios for HTTP requests
- Configures base URL from environment variables
- Provides two methods:
  - `searchMovies`: POST request to search endpoint
  - `getAllMovies`: GET request to retrieve all movies
- Handles API communication with proper TypeScript typing

**Configuration:**
- Base URL: `http://localhost:5000/api` (configurable via env)
- Content-Type: `application/json`

### Step 4: Redux Store Setup

#### Store Configuration (`store.ts`)
- Configured Redux store using Redux Toolkit
- Registered movie slice reducer
- Exported typed hooks for TypeScript support

#### Movie Slice (`movieSlice.ts`)
Implemented Redux slice with:

**State:**
- `movies`: Array of search results
- `loading`: Loading state indicator
- `error`: Error message if any
- `searchCriteria`: Current search parameters

**Actions:**
- `searchMovies`: Async thunk for API calls
- `clearMovies`: Clear search results
- `setSearchCriteria`: Update search criteria

**Reducers:**
- Handles pending, fulfilled, and rejected states for async operations
- Updates state based on API response

### Step 5: React Components

#### SearchForm Component
**Purpose**: Form for entering search criteria

**Features:**
- Three input fields: Title, Genre, Actor Name
- Form validation (at least one field required)
- Submit button with search icon
- Clear button to reset form and results
- Material-UI components with Tailwind CSS styling
- Responsive grid layout (3 columns on desktop, 1 on mobile)

**Functionality:**
- Collects user input
- Validates that at least one field is filled
- Dispatches `searchMovies` action with criteria
- Handles form submission and clearing

#### MovieCard Component
**Purpose**: Display individual movie information

**Features:**
- Movie title as heading
- Genre and release year as chips
- Movie description
- List of actors with chips
- Hover effect for better UX
- Material-UI Card component

**Styling:**
- Uses Tailwind CSS for layout and spacing
- Material-UI for components and theming
- Responsive design

#### MovieList Component
**Purpose**: Display search results

**Features:**
- Shows loading spinner during API calls
- Displays error messages if search fails
- Shows empty state message when no results
- Renders movie count
- Grid layout (responsive: 1 column mobile, 2 tablet, 3 desktop)
- Uses MovieCard component for each movie

**State Management:**
- Connects to Redux store using `useSelector`
- Reads movies, loading, and error states

### Step 6: Main Application

#### App Component
**Purpose**: Main application layout

**Structure:**
- Material-UI Container for centered content
- Header with title and subtitle
- SearchForm component
- MovieList component

**Styling:**
- Uses Tailwind CSS classes
- Material-UI Typography for text
- Responsive padding and spacing

#### main.tsx
**Purpose**: Application entry point

**Configuration:**
- Renders App component
- Wraps with Redux Provider
- Applies global styles
- Uses React StrictMode for development

### Step 7: Styling Configuration

#### Tailwind CSS
- Configured in `tailwind.config.js`
- Content paths: `index.html` and all `src/**/*.{js,ts,jsx,tsx}` files
- Custom theme extensions available

#### Global Styles (`index.css`)
- Tailwind directives (@tailwind base, components, utilities)
- Font family configuration
- Code font settings

#### Material-UI Integration
- Components use MUI theming
- Combined with Tailwind for utility classes
- Consistent design system

### Step 8: Build Configuration

#### Vite Configuration
- React plugin enabled
- Development server on port 3000
- Proxy configuration for API calls (`/api` → `http://localhost:5000`)

#### TypeScript Configuration
- Strict mode enabled
- React JSX transform
- ES2020 target
- React types included

## Component Hierarchy

```
App
├── Container (MUI)
│   ├── Header (Typography)
│   ├── SearchForm
│   │   ├── TextField (Title)
│   │   ├── TextField (Genre)
│   │   ├── TextField (Actor)
│   │   ├── Button (Search)
│   │   └── Button (Clear)
│   └── MovieList
│       └── MovieCard (for each movie)
│           ├── Title
│           ├── Chips (Genre, Year)
│           ├── Description
│           └── Actor Chips
```

## State Flow

1. **User Input**: User enters search criteria in SearchForm
2. **Form Submit**: SearchForm dispatches `searchMovies` action
3. **Redux Thunk**: Async thunk calls `movieService.searchMovies()`
4. **API Call**: Axios makes HTTP request to backend
5. **Response**: Backend returns movie data
6. **State Update**: Redux slice updates state with results
7. **UI Update**: MovieList re-renders with new data

## Running the Frontend

1. Install dependencies:
   ```bash
   npm install
   ```

2. Create `.env` file (optional):
   ```
   VITE_API_URL=http://localhost:5000/api
   ```

3. Start development server:
   ```bash
   npm run dev
   ```

4. Application will be available at `http://localhost:3000`

## Build for Production

```bash
npm run build
```

Output will be in `dist/` directory, ready for deployment.

## Features

### Search Functionality
- Search by title (partial match, case-insensitive)
- Search by genre (partial match, case-insensitive)
- Search by actor name (partial match, case-insensitive)
- Multiple criteria can be combined (AND logic)
- Real-time loading states
- Error handling and display

### User Experience
- Responsive design (mobile, tablet, desktop)
- Loading indicators
- Error messages
- Empty state handling
- Hover effects
- Clean, modern UI

### Code Quality
- TypeScript for type safety
- Component-based architecture
- Separation of concerns (services, store, components)
- Reusable components
- Proper error handling

## Future Enhancements

Potential improvements:
- Pagination for large result sets
- Advanced filtering options
- Movie detail view
- Actor detail view
- Favorites/bookmarks
- Search history
- Unit tests
- E2E tests
