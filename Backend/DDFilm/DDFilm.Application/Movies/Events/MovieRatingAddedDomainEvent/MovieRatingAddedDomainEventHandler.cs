using DDFilm.Application.Common.Clients;
using DDFilm.Application.Common.Interfaces.Caching;
using DDFilm.Contracts.Hubs;
using DDFilm.Domain.MovieAggregate.Events;
using MediatR;


namespace DDFilm.Application.Movies.Events.MovieRatingAddedDomainEvent
{
    internal sealed class MovieRatingAddedDomainEventHandler
        : INotificationHandler<MovieRatingUpdated>
    {
        private readonly IRatingNotifier _ratingNotifier;
        private readonly IRedisCacheService _redisCacheService;

        public MovieRatingAddedDomainEventHandler(IRatingNotifier ratingNotifier, 
            IRedisCacheService redisCacheService)
        {
            _ratingNotifier = ratingNotifier;
            _redisCacheService = redisCacheService;
        }

        public async Task Handle(MovieRatingUpdated notification, CancellationToken cancellationToken)
        {
            var ratingProgress = new RatingProgress(notification.ApplicationUserId, notification.RatingValue);
            
            var cacheKey = $"Session:{notification.SessionId}:Ratings";

            var existingRatings = await _redisCacheService.GetDataAsync<List<RatingProgress>>(cacheKey) ?? new List<RatingProgress>();
            existingRatings.Add(ratingProgress);

            await _redisCacheService.SetDataAsync(cacheKey, existingRatings);

            await _ratingNotifier.NotifyRatingProgressToRatingHubAsync(notification.SessionId, ratingProgress);
            await _ratingNotifier.NotifyRatingProgressToSessionHubAsync(notification.SessionId, notification.ApplicationUserId);
        }
    }
}
