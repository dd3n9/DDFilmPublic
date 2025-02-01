using DDFilm.Domain.Common.Models;

namespace DDFilm.Domain.ApplicationUserAggregate.Events
{
    public class UserFriendRemoved(ApplicationUser user, ApplicationUser friend) : IDomainEvent;
}
