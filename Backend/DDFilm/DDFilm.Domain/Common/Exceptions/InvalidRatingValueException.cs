using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class InvalidRatingValueException : DDFilmException
    {
        public InvalidRatingValueException() 
            : base("Invalid rating! Rating should be in the range from 1 to 5", StatusCodes.Status400BadRequest)
        {
        }
    }
}
