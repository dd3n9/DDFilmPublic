export interface ISessionDto {
  id: string;
  sessionName: string;
  participantLimit: number;
  participantsCount: number;
  ownerName: string;
}

export interface ISessionMovieDto {
  sessionMovieId: string;
  tmdbId: number;
  sessionName: string;
  movieTitle: string;
  averageRating: number;
  addedByUserName: string;
  isWatching: boolean;
  ratings: RatingDTO[];
  watchedAt?: string;
}

export interface ISessionParticipantDto {
  sessionId: string;
  userName: string;
  userId: string;
  role: SessionRole;
}

export interface RatingDTO {
  userName: string;
  rating: number;
}

export enum SessionRole {
  Owner,
  User,
}

export interface RatingProgress {
  userId: string;
  rating: number;
}
