using DDFilm.Domain.SessionAggregate;
using DDFilm.Domain.SessionAggregate.ValueObjects;

namespace DDFilm.Domain.Repositories
{
    public interface ISessionRepository
    {
        Task<Session> GetByIdAsync(SessionId sessionId, CancellationToken cancellationToken);
        Task AddAsync(Session session, CancellationToken cancellationToken);
        Task UpdateAsync(Session session, CancellationToken cancellationToken);
        Task DeleteAsync(Session sessionId, CancellationToken cancellationToken);
    }
}
