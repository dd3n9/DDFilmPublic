using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.MovieAggregate.ValueObjects;
using DDFilm.Domain.SessionAggregate.Entities;
using DDFilm.Domain.SessionAggregate.ValueObjects;

namespace DDFilm.Domain.Factories.SessionMovies
{
    public interface ISessionMovieFactory
    {
        Task<SessionMovie> CreateAsync(TmdbId tmdbId,
            SessionId sessionId,
            MovieTitle movieTitle,
            ApplicationUserId userId, 
            CancellationToken cancellationToken);
    }
}
