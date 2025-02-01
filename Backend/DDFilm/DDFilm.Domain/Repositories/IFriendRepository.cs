using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;

namespace DDFilm.Domain.Repositories
{
    public interface IFriendRepository
    {
        Task<bool> IsAlreadyFriendAsync(
            ApplicationUserId userId,
            ApplicationUserId friendId, 
            CancellationToken cancellationToken );
    }
}
