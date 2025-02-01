using DDFilm.Application.DTO;
using DDFilm.Domain.SessionAggregate.ValueObjects;
using DDFilm.Infrastructure.EF.Models;

namespace DDFilm.Infrastructure.EF.ModelExtensions
{
    internal static class SessionExtensions
    {
        public static SessionDto AsDto(this SessionReadModel model)
        {
            var owner = model.Participants?.FirstOrDefault(p => p.Role == SessionRole.Owner.ToString());
            return new SessionDto
            {
                Id = model.Id,
                SessionName = model.SessionName,
                Participants = model.Participants?.Select(p => p.AsDto()).ToList(),
                OwnerId = owner?.UserId,
                OwnerName = owner?.ApplicationUser?.UserName,
                Settings = new SessionSettingsDto
                {
                    ParticipantLimit = model.Settings.ParticipantLimit,
                    RequiredMoviesPerUser = model.Settings.RequiredMoviesPerUser
                }
            };
        }

        public static SessionParticipantDto AsDto(this SessionParticipantReadModel model)
            => new()
            {
                SessionId = model.SessionId,
                UserId = model.UserId,
                UserName = model.ApplicationUser.UserName,
                Role = model.Role
            };

        public static SessionMovieDto AsDto(this SessionMovieReadModel model)
            => new()
            {
                SessionMovieId = model.Id,
                SessionId = model.SessionId,
                MovieId = model.MovieId,
                TmdbId = model.Movie.TmdbId,
                SessionName = model.Session.SessionName,
                MovieTitle = model.Movie.Title,
                AddedByUserId = model.AddedByUserId,
                IsWatched = model.IsWatched,
                AddedByUserName = model.ApplicationUser.UserName,
                AverageRating = model.Movie.Ratings != null && model.Movie.Ratings.Any()
                    ? model.Movie.Ratings.Average(r => r.Rating)
                    : null,
                Ratings = model.Movie.Ratings != null && model.Movie.Ratings.Any()
                    ? model.Movie.Ratings.Select(m => m.AsDto()).ToList()
                    : null,
                WatchedAt = model.Movie.Ratings?
                    .OrderBy(r => r.CreatedAt)
                    .FirstOrDefault()?.CreatedAt,
            };

        public static MovieRatingDto AsDto(this MovieRatingReadModel model)
            => new()
            {
                MovieRatingId = model.Id,
                SessionId = model.SessionId,
                MovieId = model.MovieId,
                UserId = model.UserId, 
                UserName = model.User.UserName,
                Rating = model.Rating
            };
    }
}
