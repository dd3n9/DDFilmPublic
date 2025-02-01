using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class EmptyJwtIdException : DDFilmException
    {
        public EmptyJwtIdException()
            : base("JwtId cannot be empty", StatusCodes.Status400BadRequest)
        {
            
        }
    }
}
