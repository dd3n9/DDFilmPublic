using DDFilm.Domain.ApplicationUserAggregate;
using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.MovieAggregate;
using DDFilm.Domain.MovieAggregate.Entities;
using DDFilm.Domain.Repositories;
using DDFilm.Domain.SessionAggregate.ValueObjects;
using FluentResults;

namespace DDFilm.Domain.Services
{
    public sealed class MovieDomainService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieDomainService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Result> RateMovieAsync(Movie movie, ApplicationUser user, RatingValue rating, CancellationToken cancellationToken)
        {
            if(HasMovieNullUserRating(movie, user.Id))
            {
                var result = movie.UpdateRating(user.Id, rating);
                if (result.IsFailed)
                    return result;
            }
            else
            {
                var movieRating = MovieRating.Create(user.Id, null, rating);
                var result = movie.AddRating(movieRating);
                if (result.IsFailed)
                    return result;
            }

            await _movieRepository.UpdateAsync(movie, cancellationToken);
            return Result.Ok();
        }

        private bool HasMovieNullUserRating(Movie movie, ApplicationUserId userId)
        {
            return movie.Ratings.Any(r => r.UserId == userId && r.Rating is null);
        }
    }
}
