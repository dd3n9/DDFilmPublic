using DDFilm.Domain.ApplicationUserAggregate;
using DDFilm.Domain.Common.Models;
using DDFilm.Domain.SessionAggregate.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class SessionParticipantAlreadyExistsException : DDFilmException
    {
        public ApplicationUser User { get; }
        public SessionName SessionName { get; }

        public SessionParticipantAlreadyExistsException(ApplicationUser user, SessionName sessionName) 
            : base($"User {user.UserName} is already a member of the session '{sessionName}'",
                  StatusCodes.Status409Conflict)
        {
            User = user;
            SessionName = sessionName;
        }
    }
}
