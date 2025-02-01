using DDFilm.Application.Common.Clients;
using DDFilm.Contracts.Hubs;
using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.SessionAggregate.ValueObjects;
using DDFilm.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace DDFilm.Infrastructure.Common.Services.Clients
{
    internal sealed class RatingNotifier : IRatingNotifier
    {
        private readonly IHubContext<RatingHub, IRatingClient> _rattingHubContext;
        private readonly IHubContext<SessionHub, ISessionClient> _sessionHubContext;

        public RatingNotifier(IHubContext<RatingHub, IRatingClient> rattingHubContext,
            IHubContext<SessionHub, ISessionClient> sessionHubContext)
        {
            _rattingHubContext = rattingHubContext;
            _sessionHubContext = sessionHubContext;
        }

        public async Task NotifyRatingProgressToRatingHubAsync(SessionId sessionId, RatingProgress ratingProgress)
        {
            await _rattingHubContext.Clients
                .Group(sessionId.Value.ToString() + "-ratings").ReceiveNewRating(ratingProgress);
        }

        public async Task NotifyRatingProgressToSessionHubAsync(SessionId sessionId, ApplicationUserId userId)
        {
            await _sessionHubContext.Clients
                .Group(sessionId.Value.ToString())
                .RatingGiven(userId);
        }
    }
}
