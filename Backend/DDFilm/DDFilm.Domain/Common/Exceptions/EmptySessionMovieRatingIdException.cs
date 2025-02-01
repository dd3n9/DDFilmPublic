using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class EmptySessionMovieRatingIdException : DDFilmException
    {
        public EmptySessionMovieRatingIdException()
            : base("Session Movie Rating ID cannot be empty.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
