using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.Common.Models;
using DDFilm.Domain.MovieAggregate.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class UserNotAuthorizedToRemoveMovieException : DDFilmException
    {
        public ApplicationUserId UserId { get; }
        public MovieTitle MovieTitle { get; }
        public UserNotAuthorizedToRemoveMovieException(ApplicationUserId userId, MovieTitle movieTitle)
            : base($"User with ID {userId.Value} is not authorized to remove the movie '{movieTitle.Value}' from the session.",
                  StatusCodes.Status403Forbidden)
        {
            UserId = userId;
            MovieTitle = movieTitle;
        }
    }
}
