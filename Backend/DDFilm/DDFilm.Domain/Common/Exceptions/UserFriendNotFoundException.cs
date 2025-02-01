using DDFilm.Domain.ApplicationUserAggregate;
using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class UserFriendNotFoundException : DDFilmException
    {
        public ApplicationUser User { get; }

        public UserFriendNotFoundException(ApplicationUser user)
            : base($"User '{user.UserName}' is not your friend", StatusCodes.Status404NotFound)
        {
            User = user;
        }
    }
}
