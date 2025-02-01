using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.Common.Models;
using DDFilm.Domain.MovieAggregate.ValueObjects;
using DDFilm.Domain.SessionAggregate.ValueObjects;

namespace DDFilm.Domain.MovieAggregate.Entities
{
    public sealed class MovieRating : Entity<MovieRatingId>
    {
        public ApplicationUserId UserId { get; set; }
        public SessionId? SessionId { get; set; } = null;
        public RatingValue? Rating { get; set; }

        private MovieRating(MovieRatingId movieRatingId, 
            ApplicationUserId userId,
            SessionId? sessionId,
            RatingValue? rating) : base(movieRatingId)
        {
            UserId = userId;
            SessionId = sessionId; 
            Rating = rating;
        }

        private MovieRating() { }

        public static MovieRating Create(ApplicationUserId userId, 
            SessionId? sessionId, 
            RatingValue? ratingValue)
        {
            var movieRating = new MovieRating(
                MovieRatingId.CreateUnique(),
                userId,
                sessionId,
                ratingValue
                );

            return movieRating;
        }
    }
}
