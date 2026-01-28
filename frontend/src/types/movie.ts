export interface Actor {
  id: number;
  name: string;
  dateOfBirth: string;
}

export interface Movie {
  id: number;
  title: string;
  description: string;
  releaseYear: number;
  genre: string;
  actors: Actor[];
}

export interface MovieSearchRequest {
  title?: string;
  genre?: string;
  actorName?: string;
}
