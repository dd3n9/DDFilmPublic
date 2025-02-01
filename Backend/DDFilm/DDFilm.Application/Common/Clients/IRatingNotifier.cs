using DDFilm.Contracts.Hubs;
using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.SessionAggregate.ValueObjects;

namespace DDFilm.Application.Common.Clients
{
    public interface IRatingNotifier
    {
        Task NotifyRatingProgressToRatingHubAsync(SessionId sessionId, RatingProgress ratingProgress);
        Task NotifyRatingProgressToSessionHubAsync(SessionId sessionId, ApplicationUserId userId);
    }
}
