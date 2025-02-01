using DDFilm.Domain.Common.Models;
using DDFilm.Domain.MovieAggregate.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class SessionMovieAlreadyExistsException : DDFilmException
    {
        public MovieTitle MovieTitle { get; }
        public SessionMovieAlreadyExistsException(MovieTitle movieTitle)
            : base($"Film '{movieTitle}' has already been added to the session ",
                  StatusCodes.Status409Conflict)
        {
            MovieTitle = movieTitle;
        }
    }
}
