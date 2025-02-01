using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class EmptyTokenException : DDFilmException
    {
        public EmptyTokenException()
            : base("Token in RefreshToken cannot be empty", StatusCodes.Status400BadRequest)
        {
        }
    }
}
