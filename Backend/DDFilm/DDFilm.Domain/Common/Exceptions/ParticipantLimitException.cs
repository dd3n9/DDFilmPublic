using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class ParticipantLimitException : DDFilmException
    {
        public ParticipantLimitException() 
            : base("The limit of participants should be from 1 to 10.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
