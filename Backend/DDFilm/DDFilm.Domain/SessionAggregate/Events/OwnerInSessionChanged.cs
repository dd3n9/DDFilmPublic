using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.Common.Models;
using DDFilm.Domain.SessionAggregate.ValueObjects;

namespace DDFilm.Domain.SessionAggregate.Events
{
    public record OwnerInSessionChanged(SessionName SessionName, ApplicationUserId UserId) : IDomainEvent;
}
