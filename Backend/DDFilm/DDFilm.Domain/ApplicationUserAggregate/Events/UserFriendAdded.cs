using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.Common.Models;

namespace DDFilm.Domain.ApplicationUserAggregate.Events
{
    public record UserFriendAdded(ApplicationUserId userId, ApplicationUserId friendId) : IDomainEvent;
}
