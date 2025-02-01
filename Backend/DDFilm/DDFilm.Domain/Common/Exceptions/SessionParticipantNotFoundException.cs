using DDFilm.Domain.Common.Models;
using DDFilm.Domain.SessionAggregate.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class SessionParticipantNotFoundException : DDFilmException
    {
        public SessionName SessionName { get; }

        public SessionParticipantNotFoundException(SessionName sessionName) 
            : base($"User is not a member of the session '{sessionName}'",
                  StatusCodes.Status404NotFound)
        {
            SessionName = sessionName;
        }
    }
}
