using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class EmptyUserFriendIdException : DDFilmException
    {
        public EmptyUserFriendIdException() 
            : base("User friend ID cannot be empty.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
