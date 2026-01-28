import { useState } from 'react';
import { useDispatch } from 'react-redux';
import { Box, TextField, Button, Paper, Typography } from '@mui/material';
import { Search as SearchIcon } from '@mui/icons-material';
import { AppDispatch } from '../store/store';
import { searchMovies } from '../store/movieSlice';
import { MovieSearchRequest } from '../types/movie';

const SearchForm = () => {
  const dispatch = useDispatch<AppDispatch>();
  const [title, setTitle] = useState('');
  const [genre, setGenre] = useState('');
  const [actorName, setActorName] = useState('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    
    const searchCriteria: MovieSearchRequest = {};
    if (title.trim()) searchCriteria.title = title.trim();
    if (genre.trim()) searchCriteria.genre = genre.trim();
    if (actorName.trim()) searchCriteria.actorName = actorName.trim();

    // At least one field must be filled
    if (Object.keys(searchCriteria).length === 0) {
      alert('Please enter at least one search criteria');
      return;
    }

    dispatch(searchMovies(searchCriteria));
  };

  const handleClear = () => {
    setTitle('');
    setGenre('');
    setActorName('');
    // Don't search on clear, just reset the form
  };

  return (
    <Paper elevation={3} className="p-6 mb-8">
      <Typography variant="h5" component="h2" className="mb-4">
        Search Movies
      </Typography>
      <form onSubmit={handleSubmit}>
        <Box className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-4">
          <TextField
            label="Title"
            variant="outlined"
            fullWidth
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            placeholder="Enter movie title"
          />
          <TextField
            label="Genre"
            variant="outlined"
            fullWidth
            value={genre}
            onChange={(e) => setGenre(e.target.value)}
            placeholder="Enter genre"
          />
          <TextField
            label="Actor Name"
            variant="outlined"
            fullWidth
            value={actorName}
            onChange={(e) => setActorName(e.target.value)}
            placeholder="Enter actor name"
          />
        </Box>
        <Box className="flex gap-4">
          <Button
            type="submit"
            variant="contained"
            color="primary"
            startIcon={<SearchIcon />}
            className="flex-1"
          >
            Search
          </Button>
          <Button
            type="button"
            variant="outlined"
            onClick={handleClear}
            className="flex-1"
          >
            Clear
          </Button>
        </Box>
      </form>
    </Paper>
  );
};

export default SearchForm;
