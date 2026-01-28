import { createSlice, createAsyncThunk, PayloadAction } from '@reduxjs/toolkit';
import { Movie, MovieSearchRequest } from '../types/movie';
import { movieService } from '../services/movieService';

interface MovieState {
  movies: Movie[];
  loading: boolean;
  error: string | null;
  searchCriteria: MovieSearchRequest;
}

const initialState: MovieState = {
  movies: [],
  loading: false,
  error: null,
  searchCriteria: {},
};

// Async thunk for searching movies
export const searchMovies = createAsyncThunk(
  'movies/search',
  async (searchCriteria: MovieSearchRequest, { rejectWithValue }) => {
    try {
      console.log('Searching with criteria:', searchCriteria);
      const movies = await movieService.searchMovies(searchCriteria);
      console.log('Search results:', movies);
      return movies;
    } catch (error: any) {
      console.error('Search error:', error);
      const errorMessage = error.response?.data || error.message || 'Failed to search movies';
      return rejectWithValue(errorMessage);
    }
  }
);

const movieSlice = createSlice({
  name: 'movies',
  initialState,
  reducers: {
    clearMovies: (state) => {
      state.movies = [];
      state.searchCriteria = {};
    },
    setSearchCriteria: (state, action: PayloadAction<MovieSearchRequest>) => {
      state.searchCriteria = action.payload;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(searchMovies.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(searchMovies.fulfilled, (state, action) => {
        state.loading = false;
        state.movies = action.payload;
      })
      .addCase(searchMovies.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      });
  },
});

export const { clearMovies, setSearchCriteria } = movieSlice.actions;
export default movieSlice.reducer;
