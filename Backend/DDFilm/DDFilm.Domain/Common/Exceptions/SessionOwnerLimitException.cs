using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class SessionOwnerLimitException : DDFilmException
    {
        public SessionOwnerLimitException() 
            : base("There can be no more than one owner in a session", StatusCodes.Status400BadRequest)
        {
        }
    }
}
