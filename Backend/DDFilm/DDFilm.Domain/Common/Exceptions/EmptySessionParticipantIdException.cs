using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class EmptySessionParticipantIdException : DDFilmException
    {
        public EmptySessionParticipantIdException() 
            : base("Session Participant ID cannot be empty.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
