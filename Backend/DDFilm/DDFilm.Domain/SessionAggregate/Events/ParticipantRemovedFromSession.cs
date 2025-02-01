using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.Common.Models;

namespace DDFilm.Domain.SessionAggregate.Events
{
    public record ParticipantRemovedFromSession(Session Session, ApplicationUserId UserId) : IDomainEvent;
}
