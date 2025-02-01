using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;


namespace DDFilm.Domain.Common.Exceptions
{
    public class EmptyMovieTitleException : DDFilmException
    {
        public EmptyMovieTitleException() 
            : base("Movie Title cannot be empty", StatusCodes.Status400BadRequest)
        {
        }
    }
}
