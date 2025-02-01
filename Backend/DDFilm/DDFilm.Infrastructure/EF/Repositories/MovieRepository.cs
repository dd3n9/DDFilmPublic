using DDFilm.Domain.MovieAggregate;
using DDFilm.Domain.MovieAggregate.ValueObjects;
using DDFilm.Domain.Repositories;
using DDFilm.Domain.SessionAggregate.ValueObjects;
using DDFilm.Infrastructure.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace DDFilm.Infrastructure.EF.Repositories
{
    internal sealed class MovieRepository : IMovieRepository
    {
        private readonly DbSet<Movie> _movie;
        private readonly WriteDbContext _writeDbContext;

        public MovieRepository(WriteDbContext writeDbContext)
        {
            _movie = writeDbContext.Movies;
            _writeDbContext = writeDbContext;
        }

        public async Task<Movie?> GetByIdAsync(MovieId movieId, CancellationToken cancellationToken)
        {
            var result = await _movie
                .Include(m => m.Ratings)
                .SingleOrDefaultAsync(m => m.Id == movieId, cancellationToken);   

            return result;
        }

        public async Task<Movie?> GetByTmdbIdAsync(TmdbId tmdbId, CancellationToken cancellationToken)
        {
            var result = await _movie
                .Include(m => m.Ratings)
                .SingleOrDefaultAsync(m => m.TmdbId == tmdbId, cancellationToken);

            return result;
        }
        public async Task<Movie?> GetMovieWithLatestRatingInSessionAsync(SessionId sessionId, CancellationToken cancellationToken)
        {
            var result = await _movie
                .Where(m => m.Ratings.Any(r => r.SessionId == sessionId))
                .Include(m => m.Ratings)
                .OrderByDescending(m => m.Ratings.Max(mr => mr.CreatedAt))
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }

        public async Task AddAsync(Movie movie, CancellationToken cancellationToken)
        {
            await _movie.AddAsync(movie, cancellationToken);
            await _writeDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Movie movie, CancellationToken cancellationToken)
        {
            _movie.Update(movie);
            await _writeDbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(Movie movie, CancellationToken cancellationToken)
        {
            _movie.Remove(movie);
            await _writeDbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
