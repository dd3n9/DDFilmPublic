using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class UserFriendAlreadyExistsException : DDFilmException
    {
        public UserFriendAlreadyExistsException()
            : base($"User is already in the friends list", StatusCodes.Status409Conflict)
        {
        }
    }
}
