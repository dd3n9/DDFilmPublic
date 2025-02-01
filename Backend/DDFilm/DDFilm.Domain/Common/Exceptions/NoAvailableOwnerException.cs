using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class NoAvailableOwnerException : DDFilmException
    {
        public string SessionName { get; }
        public NoAvailableOwnerException(string sessionName)
        : base($"Cannot find a new owner for the session '{sessionName}' because there are no eligible participants.",
              StatusCodes.Status404NotFound)
        {
            SessionName = sessionName;
        }
    }
}
