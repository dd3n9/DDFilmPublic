using DDFilm.Application.Common.Helpers;
using DDFilm.Application.Common.Interfaces.Services;
using DDFilm.Application.DTO;
using DDFilm.Contracts.Common;
using DDFilm.Infrastructure.EF.Context;
using DDFilm.Infrastructure.EF.ModelExtensions;
using DDFilm.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace DDFilm.Infrastructure.EF.Services
{
    internal sealed class SessionMovieReadService : ISessionMovieReadService
    {
        private readonly DbSet<SessionReadModel> _session;

        public SessionMovieReadService(ReadDbContext readDbContext)
        {
            _session = readDbContext.Sessions;
        }

        public async Task<IEnumerable<SessionMovieDto>> GetMoviesBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken)
        {
            var result = await _session
               .Where(s => s.Id == sessionId)
               .SelectMany(s => s.SessionMovies)
               .Where(sm => sm.SessionId != null)
               .Include(sm => sm.Movie)
                    .ThenInclude(m => m.Ratings)
                        .ThenInclude(rm => rm.User)
               .Include(sm => sm.Session)
               .Include(sm => sm.ApplicationUser)
               .AsSplitQuery()
               .Select(sm => sm.AsDto())
               .ToListAsync(cancellationToken);

            return result;
        }

        public async Task<PaginatedList<SessionMovieDto>> GetUnwatchedMoviesBySessionIdAsync(PaginationParams paginationParams, Guid sessionId, CancellationToken cancellationToken)
        {
            var getSessionMovieQuery = _session
                .Where(s => s.Id == sessionId)
                .SelectMany(s => s.SessionMovies)
                .Where(sm => sm.IsWatched == false)
                .Include(sm => sm.Movie)
                    .ThenInclude(m => m.Ratings)
                        .ThenInclude(rm => rm.User)
               .Include(sm => sm.Session)
               .Include(sm => sm.ApplicationUser)
               .Select(sm => sm.AsDto());

            var paginatedResult = await CollectionHelper<SessionMovieDto>.ToPaginatedList(getSessionMovieQuery,
                paginationParams.PageNumber, paginationParams.PageSize);

            return paginatedResult;
        }

        public async Task<IEnumerable<SessionMovieDto>> GetUnwatchedMoviesBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken)
        {
            var getSessionMovieResult = _session
                .Where(s => s.Id == sessionId)
                .SelectMany(s => s.SessionMovies)
                .Where(sm => sm.IsWatched == false)
                .Include(sm => sm.Movie)
                    .ThenInclude(m => m.Ratings)
                        .ThenInclude(rm => rm.User)
               .Include(sm => sm.Session)
               .Include(sm => sm.ApplicationUser)
               .Select(sm => sm.AsDto());

            return getSessionMovieResult;
        }

        public async Task<PaginatedList<SessionMovieDto>> GetWatchedMoviesBySessionIdAsync(PaginationParams paginationParams, Guid sessionId, CancellationToken cancellationToken)
        {
            var getSessionMovieQuery = _session
                .Where(s => s.Id == sessionId)
                .SelectMany(s => s.SessionMovies)
                .Where(sm => sm.IsWatched == true && sm.Movie.Ratings.Any(mr => mr.Rating != null))
                .Include(sm => sm.Movie)
                    .ThenInclude(m => m.Ratings)
                        .ThenInclude(rm => rm.User)
               .Include(sm => sm.Session)
               .Include(sm => sm.ApplicationUser)
               .Select(sm => sm.AsDto());

            var paginatedResult = await CollectionHelper<SessionMovieDto>.ToPaginatedList(getSessionMovieQuery,
                paginationParams.PageNumber, paginationParams.PageSize);


            return paginatedResult;
        }

        public async Task<SessionMovieDto> GetWatchingMovieBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken)
        {
            var getWatchingMovieResult = await _session
                .Where(s => s.Id == sessionId)
                .SelectMany(s => s.SessionMovies)
                .Where(sm => sm.IsWatched == true && sm.Movie.Ratings.Any(mr => mr.Rating == null))
                .Include(sm => sm.Movie)
                    .ThenInclude(m => m.Ratings)
                        .ThenInclude(rm => rm.User)
               .Include(sm => sm.Session)
               .Include(sm => sm.ApplicationUser)
               .Select(sm => sm.AsDto())
               .FirstOrDefaultAsync(cancellationToken);

            return getWatchingMovieResult;
        }

        public async Task<bool> IsCurrentlyWatchingMovie(Guid sessionId, CancellationToken cancellationToken)
        {
            var result = await _session
                .Where(s => s.Id == sessionId)
                .SelectMany(s => s.SessionMovies)
                .Where(sm => sm.IsWatched 
                    && sm.Movie.Ratings != null
                    && sm.Movie.Ratings.All(mr => mr.Rating != null))
                .AnyAsync(cancellationToken);

            return result;
        }
    }
}
