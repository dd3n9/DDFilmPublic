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
    internal sealed class SessionReadService : ISessionReadService
    {
        private readonly DbSet<SessionReadModel> _session;

        public SessionReadService(ReadDbContext context)
        {
            _session = context.Sessions;
        }

        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken)
            => await _session.AnyAsync(s => s.Id == id, cancellationToken);

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
            => await _session.AnyAsync(s => s.SessionName == name, cancellationToken);

        public async Task<IEnumerable<SessionDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _session
                .Include(s => s.Participants)
                    .ThenInclude(p => p.ApplicationUser)
                .Include(s => s.Settings)
                .Select(s => s.AsDto())
                .ToListAsync();

            return result;
        }

        public async Task<PaginatedList<SessionDto>> GetAllSortedAsync(PaginationParams paginationParams, CancellationToken cancellationToken)
        {
            var getSessionQuery = _session
                .Include(s => s.Participants)
                    .ThenInclude(p => p.ApplicationUser)
                .Include(s => s.Settings)
                .Select(s => s.AsDto());

            var paginatedResult = await CollectionHelper<SessionDto>.ToPaginatedList(getSessionQuery,
                paginationParams.PageNumber, paginationParams.PageSize);

            return paginatedResult;
        }

        public async Task<SessionDto> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var result = await _session
                .Where(s => s.SessionName == name)
                .Include(s => s.Participants)
                    .ThenInclude(p => p.ApplicationUser)
                .Include(s => s.Settings)
                .Select(s => s.AsDto())
                .SingleOrDefaultAsync();

            return result;
        }

        public async Task<IEnumerable<SessionDto>> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
        {
            var result = await _session
                .Where(s => s.Participants.Any(p => p.UserId == userId))
                .Include(s => s.Participants)
                    .ThenInclude(p => p.ApplicationUser)
                .Include(s => s.Settings)
                .Select(s => s.AsDto())
                .ToListAsync();

            return result;
        }

        public async Task<PaginatedList<SessionDto>> GetByUserIdSortedAsync(PaginationParams paginationParams, string userId, CancellationToken cancellationToken)
        {
            var getSessionQuery = _session
                .Where(s => s.Participants.Any(p => p.UserId == userId))
                .Include(s => s.Participants)
                    .ThenInclude(p => p.ApplicationUser)
                .Include(s => s.Settings)
                .Select(s => s.AsDto());

            var paginatedResult = await CollectionHelper<SessionDto>.ToPaginatedList(getSessionQuery,
               paginationParams.PageNumber, paginationParams.PageSize);

            return paginatedResult;
        }
    }
}
