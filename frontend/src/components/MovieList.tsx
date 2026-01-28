import { useSelector } from 'react-redux';
import { Box, Typography, CircularProgress, Alert, Grid } from '@mui/material';
import { RootState } from '../store/store';
import MovieCard from './MovieCard';

const MovieList = () => {
  const { movies, loading, error } = useSelector((state: RootState) => state.movies);

  if (loading) {
    return (
      <Box className="flex justify-center items-center py-12">
        <CircularProgress />
      </Box>
    );
  }

  if (error) {
    return (
      <Alert severity="error" className="mb-4">
        {error}
      </Alert>
    );
  }

  if (movies.length === 0) {
    return (
      <Box className="text-center py-12">
        <Typography variant="h6" color="text.secondary">
          No movies found. Try searching with different criteria.
        </Typography>
      </Box>
    );
  }

  return (
    <Box>
      <Typography variant="h5" component="h2" className="mb-4">
        Search Results ({movies.length})
      </Typography>
      <Grid container spacing={3}>
        {movies.map((movie) => (
          <Grid item xs={12} sm={6} md={4} key={movie.id}>
            <MovieCard movie={movie} />
          </Grid>
        ))}
      </Grid>
    </Box>
  );
};

export default MovieList;
