using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class RequiredMoviesNumberPerUserException : DDFilmException
    {
        public RequiredMoviesNumberPerUserException() 
            : base("Movies per user should be from 1 to 10.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
