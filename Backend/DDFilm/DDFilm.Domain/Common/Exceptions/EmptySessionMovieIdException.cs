using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class EmptySessionMovieIdException : DDFilmException
    {
        public EmptySessionMovieIdException()
            : base("Session Movie ID cannot be empty.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
