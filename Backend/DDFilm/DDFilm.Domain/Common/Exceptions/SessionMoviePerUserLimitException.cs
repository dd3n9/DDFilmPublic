using DDFilm.Domain.Common.Models;
using DDFilm.Domain.SessionAggregate.Entities;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class SessionMoviePerUserLimitException : DDFilmException
    {
        public SessionSettings Settings { get; }

        public SessionMoviePerUserLimitException(SessionSettings settings)
            : base($"The maximum number({ settings.RequiredMoviesPerUser }) of films for a participant has been added",
                  StatusCodes.Status400BadRequest)
        {
            Settings = settings;
        }
    }
}
