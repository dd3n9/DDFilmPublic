using DDFilm.Application.Common.Interfaces.Services;
using DDFilm.Application.DTO;
using DDFilm.Infrastructure.EF.Context;
using DDFilm.Infrastructure.EF.ModelExtensions;
using DDFilm.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace DDFilm.Infrastructure.EF.Services
{
    internal sealed class SessionParticipantReadService : ISessionParticipantReadService
    {
        private readonly DbSet<SessionReadModel> _session;

        public SessionParticipantReadService(ReadDbContext readDbContext)
        {
            _session = readDbContext.Sessions;
        }

        public async Task<IEnumerable<SessionParticipantDto>> GetBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken)
        {
            var result = await _session
                .Where(s => s.Id == sessionId)
                .SelectMany(s => s.Participants)
                .Include(sp => sp.ApplicationUser)
                .Select(s => s.AsDto())
                .ToListAsync(cancellationToken);

            return result;
        }

        public async Task<bool> IsUserParticipant(Guid sessionId, string userId, CancellationToken cancellationToken)
        {
            var session = await _session
                .Where(s => s.Id == sessionId)
                .Include(s => s.Participants)
                .FirstOrDefaultAsync();

            if(session.Participants.Any(sp => sp.UserId == userId))
            {
                return true;
            }

            return false;
        }
    }
}
