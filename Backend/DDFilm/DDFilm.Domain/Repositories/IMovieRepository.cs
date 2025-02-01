using DDFilm.Domain.MovieAggregate;
using DDFilm.Domain.MovieAggregate.ValueObjects;
using DDFilm.Domain.SessionAggregate.ValueObjects;

namespace DDFilm.Domain.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie?> GetByIdAsync(MovieId movieId, CancellationToken cancellationToken);
        Task<Movie?> GetByTmdbIdAsync(TmdbId tmdbId, CancellationToken cancellationToken);
        Task<Movie?> GetMovieWithLatestRatingInSessionAsync(SessionId sessionId, CancellationToken cancellationToken);
        Task AddAsync(Movie movie, CancellationToken cancellationToken); 
        Task UpdateAsync(Movie movie, CancellationToken cancellationToken);
        Task DeleteAsync(Movie movieId, CancellationToken cancellationToken);
    }
}
