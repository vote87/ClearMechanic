import axios from 'axios';
import { Movie, MovieSearchRequest } from '../types/movie';

const API_BASE_URL = import.meta.env.VITE_API_URL || '/api';

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

export const movieService = {
  /**
   * Searches for movies based on the provided criteria.
   * @param searchCriteria - Search parameters (title, genre, actorName)
   * @returns Promise resolving to an array of movies
   */
  async searchMovies(searchCriteria: MovieSearchRequest): Promise<Movie[]> {
    const response = await apiClient.post<Movie[]>('/movies/search', searchCriteria);
    return response.data;
  },

  /**
   * Gets all movies.
   * @returns Promise resolving to an array of all movies
   */
  async getAllMovies(): Promise<Movie[]> {
    const response = await apiClient.get<Movie[]>('/movies');
    return response.data;
  },
};
