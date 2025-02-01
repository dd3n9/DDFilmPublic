using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.Common.Models;
using DDFilm.Domain.SessionAggregate.ValueObjects;

namespace DDFilm.Domain.SessionAggregate.Entities
{
    public class SessionParticipant : Entity<SessionParticipantId>
    {
        public ApplicationUserId UserId { get; private set; }
        public SessionRole Role { get; private set; }

        public static SessionParticipant Create(ApplicationUserId userId, SessionRole role)
        {
            var sessionParticipant = new SessionParticipant(
                    SessionParticipantId.CreateUnique(),
                    userId,
                    role);

            return sessionParticipant;
        }

        private SessionParticipant()
        {      
        }

        private SessionParticipant(SessionParticipantId sessionParticipantId, 
            ApplicationUserId userId, 
            SessionRole role) : base(sessionParticipantId)
        {
            UserId = userId;
            Role = role;
        }

        internal void ChangeRole(SessionRole newRole)
        {
            Role = newRole;
        }
    }
}
