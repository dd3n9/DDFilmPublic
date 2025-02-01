using DDFilm.Domain.Common.Models;
using DDFilm.Domain.SessionAggregate.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class SessionParticipantLimitException : DDFilmException
    {
        public SessionId SessionId { get; }
        public SessionParticipantLimitException(SessionId sessionId)
            : base($"Maximum number of participants in session '{ sessionId }' has been reached",
                  StatusCodes.Status400BadRequest)
        {
            SessionId = sessionId;
        }

    }
}
