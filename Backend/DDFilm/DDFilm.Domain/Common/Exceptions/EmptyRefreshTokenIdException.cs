using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class EmptyRefreshTokenIdException : DDFilmException
    {
        public EmptyRefreshTokenIdException()
            : base("RefreshTokenId cannot be empty", StatusCodes.Status400BadRequest)
        {
            
        }
    }
}
