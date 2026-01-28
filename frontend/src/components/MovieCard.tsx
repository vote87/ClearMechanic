import { Card, CardContent, Typography, Chip, Box } from '@mui/material';
import { Movie } from '../types/movie';

interface MovieCardProps {
  movie: Movie;
}

const MovieCard = ({ movie }: MovieCardProps) => {
  return (
    <Card className="h-full hover:shadow-lg transition-shadow">
      <CardContent>
        <Typography variant="h5" component="h3" className="mb-2">
          {movie.title}
        </Typography>
        <Box className="mb-2 flex gap-2 flex-wrap">
          <Chip label={movie.genre} color="primary" size="small" />
          <Chip label={movie.releaseYear} variant="outlined" size="small" />
        </Box>
        <Typography variant="body2" color="text.secondary" className="mb-3">
          {movie.description}
        </Typography>
        <Box>
          <Typography variant="subtitle2" className="mb-1 font-semibold">
            Actors:
          </Typography>
          <Box className="flex flex-wrap gap-1">
            {movie.actors.length > 0 ? (
              movie.actors.map((actor) => (
                <Chip
                  key={actor.id}
                  label={actor.name}
                  size="small"
                  variant="outlined"
                  className="text-xs"
                />
              ))
            ) : (
              <Typography variant="caption" color="text.secondary">
                No actors listed
              </Typography>
            )}
          </Box>
        </Box>
      </CardContent>
    </Card>
  );
};

export default MovieCard;
