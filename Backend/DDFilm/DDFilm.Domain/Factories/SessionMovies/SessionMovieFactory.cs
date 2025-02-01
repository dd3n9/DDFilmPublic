using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.MovieAggregate;
using DDFilm.Domain.MovieAggregate.ValueObjects;
using DDFilm.Domain.Repositories;
using DDFilm.Domain.SessionAggregate.Entities;
using DDFilm.Domain.SessionAggregate.ValueObjects;

namespace DDFilm.Domain.Factories.SessionMovies
{
    public sealed class SessionMovieFactory : ISessionMovieFactory
    {
        private readonly IMovieRepository _movieRepository;

        public SessionMovieFactory(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<SessionMovie> CreateAsync(TmdbId tmdbId, SessionId sessionId, MovieTitle movieTitle, 
            ApplicationUserId userId, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetByTmdbIdAsync(tmdbId, cancellationToken);

            if(movie is null)
            {
                movie = Movie.Create(movieTitle, tmdbId);
                await _movieRepository.AddAsync(movie, cancellationToken);
            }

            return SessionMovie.Create(movie.Id, userId);
        }
    }
}
