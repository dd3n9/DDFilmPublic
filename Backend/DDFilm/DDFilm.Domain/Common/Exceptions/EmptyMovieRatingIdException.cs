using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;


namespace DDFilm.Domain.Common.Exceptions
{
    public class EmptyMovieRatingIdException : DDFilmException
    {
        public EmptyMovieRatingIdException()
            : base("Movie Rating ID cannot be empty.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
