using DDFilm.Application.DTO;

namespace DDFilm.Application.Common.Interfaces.Services
{
    public interface ISessionParticipantReadService
    {
        Task<IEnumerable<SessionParticipantDto>> GetBySessionIdAsync (Guid sessionId, CancellationToken cancellationToken);
        Task<bool> IsUserParticipant(Guid sessionId, string userId, CancellationToken cancellationToken);
    }
}
