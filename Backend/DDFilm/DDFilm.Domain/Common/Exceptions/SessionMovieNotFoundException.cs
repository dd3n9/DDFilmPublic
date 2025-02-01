using DDFilm.Domain.Common.Models;
using DDFilm.Domain.MovieAggregate.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class SessionMovieNotFoundException : DDFilmException
    {
        public MovieTitle MovieTitle { get; }

        public SessionMovieNotFoundException(MovieTitle movieTitle)
            : base($"Movie '{movieTitle}' was not found in session", StatusCodes.Status404NotFound)
        {
            MovieTitle = movieTitle;
        }
    }
}
