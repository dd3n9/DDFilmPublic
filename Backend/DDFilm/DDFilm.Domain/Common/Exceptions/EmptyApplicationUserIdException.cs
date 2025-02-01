using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class EmptyApplicationUserIdException : DDFilmException
    {
        public EmptyApplicationUserIdException() 
            : base("Application User Id cannot be empty", StatusCodes.Status400BadRequest)
        {
        }
    }
}
