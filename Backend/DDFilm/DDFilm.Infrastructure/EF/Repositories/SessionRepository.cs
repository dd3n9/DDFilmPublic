using DDFilm.Domain.Repositories;
using DDFilm.Domain.SessionAggregate;
using DDFilm.Domain.SessionAggregate.ValueObjects;
using DDFilm.Infrastructure.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace DDFilm.Infrastructure.EF.Repositories
{
    internal sealed class SessionRepository : ISessionRepository
    {
        private readonly DbSet<Session> _session;
        private readonly WriteDbContext _writeDbContext;

        public SessionRepository(WriteDbContext writeDbContext)
        {
            _session = writeDbContext.Sessions;
            _writeDbContext = writeDbContext;
        }

        public async Task AddAsync(Session session, CancellationToken cancellationToken)
        {
            await _session.AddAsync(session, cancellationToken);
            await _writeDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Session> GetByIdAsync(SessionId sessionId, CancellationToken cancellationToken)
        {
            var result = await _session
                .Include(s => s.Settings)
                .SingleOrDefaultAsync(s => s.Id == sessionId, cancellationToken);
            return result;
        }

        public async Task UpdateAsync(Session session, CancellationToken cancellationToken)
        {
            _session.Update(session);
            await _writeDbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(Session session, CancellationToken cancellationToken)
        {
            _session.Remove(session);
            await _writeDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
