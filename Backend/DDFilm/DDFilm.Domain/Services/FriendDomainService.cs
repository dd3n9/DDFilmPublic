using DDFilm.Domain.ApplicationUserAggregate;
using DDFilm.Domain.ApplicationUserAggregate.Entities;
using DDFilm.Domain.Common.Exceptions;
using DDFilm.Domain.Repositories;

namespace DDFilm.Domain.Services
{
    public sealed class FriendDomainService
    {
        private readonly IFriendRepository _friendRepository;

        public FriendDomainService(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public async Task StartFriendshipAsync(
            ApplicationUser user, 
            ApplicationUser friend,
            CancellationToken cancellationToken)
        {
            if(user.Id == friend.Id) 
            {
                throw new AddYourselfAsFriendException();
            }

            if(await _friendRepository.IsAlreadyFriendAsync(
                user.Id, friend.Id, cancellationToken))
            {
                throw new UserFriendAlreadyExistsException();
            }

            var userFriend = new UserFriend(user, friend);
            user.AddFriend(userFriend);

            var friendUser = new UserFriend(friend, user);
            friend.AddFriend(friendUser);
        }

        public async Task RemoveFriendAsync(
            ApplicationUser user,
            ApplicationUser friend,
            CancellationToken cancellationToken)
        {
            if (!await _friendRepository.IsAlreadyFriendAsync(
                user.Id, friend.Id, cancellationToken))
            {
                throw new UserFriendNotFoundException(friend);
            }

            user.RemoveFriend(friend);
            friend.RemoveFriend(user);
        }
    }
}
