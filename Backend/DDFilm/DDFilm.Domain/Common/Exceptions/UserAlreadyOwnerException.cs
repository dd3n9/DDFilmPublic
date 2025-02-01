using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class UserAlreadyOwnerException : DDFilmException
    {
        public UserAlreadyOwnerException()
            : base("This user is already the owner of the session.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
