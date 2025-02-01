using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.Common.Models;
using System.ComponentModel.DataAnnotations.Schema;


namespace DDFilm.Domain.ApplicationUserAggregate.Entities
{
    public sealed class UserFriend : Entity<UserFriendId>
    {
        public ApplicationUserId UserId { get; private set; }

        [NotMapped]
        public ApplicationUser User { get; private set; }

        public ApplicationUserId FriendId { get; private set; }

        [NotMapped]
        public ApplicationUser Friend { get; private set; }

        public UserFriend() { }

        public UserFriend(ApplicationUser user, ApplicationUser friend)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Friend = friend ?? throw new ArgumentNullException(nameof(friend));
            UserId = user.Id; 
            FriendId = friend.Id; 
        }
    }
}
