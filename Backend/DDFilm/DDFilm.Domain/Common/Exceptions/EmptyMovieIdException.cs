using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;


namespace DDFilm.Domain.Common.Exceptions
{
    public class EmptyMovieIdException : DDFilmException
    {
        public EmptyMovieIdException() 
            : base("Movie ID cannot be empty.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
