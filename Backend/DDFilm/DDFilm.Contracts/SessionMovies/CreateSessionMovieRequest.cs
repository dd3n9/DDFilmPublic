namespace DDFilm.Contracts.SessionMovies
{
    public record CreateSessionMovieRequest(
        int TmdbId,
        string MovieTitle
        );
}
