using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class EmptySessionNameException : DDFilmException
    {
        public EmptySessionNameException() 
            : base("Session name cannot be empty.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
