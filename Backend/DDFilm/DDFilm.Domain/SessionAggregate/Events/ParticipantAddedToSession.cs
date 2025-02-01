using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.Common.Models;

namespace DDFilm.Domain.SessionAggregate.Events
{
    public record ParticipantAddedToSession(Session session, ApplicationUserId UserId) : IDomainEvent;
}
