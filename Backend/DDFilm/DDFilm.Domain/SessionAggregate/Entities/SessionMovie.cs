using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.Common.Models;
using DDFilm.Domain.MovieAggregate.ValueObjects;
using DDFilm.Domain.SessionAggregate.ValueObjects;
using FluentResults;

namespace DDFilm.Domain.SessionAggregate.Entities
{
    public class SessionMovie : Entity<SessionMovieId>
    {
        public MovieId MovieId { get; private set; }
        public ApplicationUserId AddedByUserId { get; private set; }
        public bool IsWatched { get; private set; } = false;

          
        private SessionMovie(SessionMovieId sessionMovieId, 
            MovieId movieId,    
            ApplicationUserId addedByUserId) : base(sessionMovieId)
        {
            MovieId = movieId;
            AddedByUserId = addedByUserId ?? throw new ArgumentNullException(nameof(addedByUserId));
        }

        private SessionMovie() { }

        public static SessionMovie Create(MovieId movieId, ApplicationUserId addedByUserId)
        {
            var sessionMovie = new SessionMovie(
                SessionMovieId.CreateUnique(),
                movieId,
                addedByUserId
                );

            return sessionMovie;
        }
        public Result MarkAsWatched()
        {
            if (IsWatched)
                return Result.Fail("The movie has already been marked as watched.");

            IsWatched = true;
            return Result.Ok();
        }
    }
}
