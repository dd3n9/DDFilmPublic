using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class EmptySessionIdException : DDFilmException
    {
        public EmptySessionIdException() 
            : base("Session ID cannot be empty.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
