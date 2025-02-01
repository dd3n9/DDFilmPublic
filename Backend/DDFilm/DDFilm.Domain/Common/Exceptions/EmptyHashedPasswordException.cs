using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class EmptyHashedPasswordException : DDFilmException
    {
        public EmptyHashedPasswordException()
            : base("Password cannot be empty.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
