using DDFilm.Domain.Common.Models;
using DDFilm.Domain.MovieAggregate.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class MovieRatingAlreadyExistsException : DDFilmException
    {
        public string UserId { get; }
        public MovieId MovieId { get; }

        public MovieRatingAlreadyExistsException(string userId, MovieId movieId)
            : base($"User{userId} has already rated the film {movieId}", StatusCodes.Status409Conflict)
        {
            UserId = userId;
            MovieId = movieId;
        }
    }
}
