import { Container, Typography, Box } from '@mui/material'
import SearchForm from './components/SearchForm'
import MovieList from './components/MovieList'

function App() {
  return (
    <Container maxWidth="lg" className="py-8">
      <Box className="mb-8">
        <Typography variant="h3" component="h1" className="mb-4" align="center">
          Movie Search
        </Typography>
        <Typography variant="subtitle1" align="center" color="text.secondary">
          Search movies by title, genre, or actor name
        </Typography>
      </Box>
      <SearchForm />
      <MovieList />
    </Container>
  )
}

export default App
