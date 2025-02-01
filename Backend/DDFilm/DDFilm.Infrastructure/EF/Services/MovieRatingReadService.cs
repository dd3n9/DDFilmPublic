using DDFilm.Application.Common.Interfaces.Services;
using DDFilm.Application.DTO;
using DDFilm.Infrastructure.EF.Context;
using DDFilm.Infrastructure.EF.ModelExtensions;
using DDFilm.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace DDFilm.Infrastructure.EF.Services
{
    internal sealed class MovieRatingReadService : IMovieRatingReadService
    {
        private readonly DbSet<MovieReadModel> _movie;

        public MovieRatingReadService(ReadDbContext readDbContext)
        {
            _movie = readDbContext.Movies;
        }

        public async Task<IEnumerable<MovieRatingDto>> GetMovieRatingsBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken)
        {
            var result = await _movie
                .SelectMany(s => s.Ratings)
                .Include(mr => mr.User)
                .Where(r => r.SessionId == sessionId)
                .Select(mr => mr.AsDto())
                .ToListAsync(cancellationToken);

            return result;
        }

        public async Task<IEnumerable<MovieRatingDto>> GetMovieRatingsBySessionIdAsync(int TmdbId, Guid sessionId, CancellationToken cancellationToken)
        {
            var result = await _movie
               .Where(m => m.TmdbId == TmdbId)
               .SelectMany(s => s.Ratings)
               .Include(mr => mr.User)
               .Where(r => r.SessionId == sessionId)
               .Select(mr => mr.AsDto())
               .ToListAsync(cancellationToken);

            return result;
        }
    }
}
