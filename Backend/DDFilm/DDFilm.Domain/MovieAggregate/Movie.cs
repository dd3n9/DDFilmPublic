using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.Common.Errors;
using DDFilm.Domain.Common.Models;
using DDFilm.Domain.MovieAggregate.Entities;
using DDFilm.Domain.MovieAggregate.Events;
using DDFilm.Domain.MovieAggregate.ValueObjects;
using DDFilm.Domain.SessionAggregate.ValueObjects;
using FluentResults;

namespace DDFilm.Domain.MovieAggregate
{
    public class Movie : AggregateRoot<MovieId>
    {
        public MovieTitle Title { get; private set; }
        public TmdbId TmdbId { get; private set; }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;

        private readonly List<MovieRating> _ratings = new();
        public IReadOnlyList<MovieRating> Ratings => _ratings.AsReadOnly();

        private Movie() { }

        private Movie(MovieId id, MovieTitle title, TmdbId tmdbId) : base(id)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            TmdbId = tmdbId;
        }

        public static Movie Create(MovieTitle title, TmdbId tmdbId)
        {
            var movie = new Movie(
                MovieId.CreateUnique(),
                title, 
                tmdbId
                );

            return movie;
        }

        public Result AddRating(MovieRating rating)
        {
            var isMovieRated = _ratings.Any(mr => mr.UserId == rating.UserId);
            if (isMovieRated)
                return Result.Fail(ApplicationErrors.MovieRating.AlreadyRated);
            _ratings.Add(rating);
            return Result.Ok();
        }

        public Result? UpdateRating(ApplicationUserId userId, RatingValue rating)
        {
            var existingRating = _ratings.FirstOrDefault(r => r.UserId == userId);

            if (existingRating is null)
                return Result.Fail(ApplicationErrors.MovieRating.NotFound);

            if(existingRating.Rating is not null)
                return Result.Fail(ApplicationErrors.MovieRating.AlreadyRated);

            existingRating.Rating = rating;
            AddEvent(new MovieRatingUpdated(existingRating.SessionId, userId, rating));

            return Result.Ok();
        }

        public MovieRating? GetRatingByUser(ApplicationUserId userId)
        {
            return _ratings.SingleOrDefault(rating => rating.UserId == userId);
        }
    }                                 
}
