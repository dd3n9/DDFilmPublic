using DDFilm.Application.Common.Clients;
using DDFilm.Application.Common.Interfaces.Caching;
using DDFilm.Application.Common.Interfaces.Services;
using DDFilm.Contracts.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace DDFilm.Infrastructure.Hubs
{
    public class RatingHub : Hub<IRatingClient>
    {
        private readonly IRedisCacheService _redisService;
        private readonly IMovieRatingReadService _movieReadService;
        private readonly ISessionMovieReadService _sessionMovieReadService;

        public RatingHub(IRedisCacheService redisService,
            IMovieRatingReadService movieReadService,
            ISessionMovieReadService sessionMovieReadService)
        {
            _redisService = redisService;
            _movieReadService = movieReadService;
            _sessionMovieReadService = sessionMovieReadService;
        }

        public async Task JoinRatingRoom(UserConnection connection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.SessionId.ToString() + "-ratings");
            
            var cacheKey = $"Session:{connection.SessionId}:Ratings";
            
            var cacheRatings = await _redisService.GetDataAsync<IEnumerable<RatingProgress>>(cacheKey);




            if(cacheRatings == null || !cacheRatings.Any()) 
            {
                var watchingMovie = await _sessionMovieReadService.GetWatchingMovieBySessionIdAsync(connection.SessionId, Context.ConnectionAborted);

                var ratings = await _movieReadService.GetMovieRatingsBySessionIdAsync(watchingMovie.TmdbId, connection.SessionId, Context.ConnectionAborted);

                var ratingsProgress = ratings
                    .Where(r => r.Rating.HasValue)
                    .Select(r => new RatingProgress(r.UserId , r.Rating))
                    .ToList();

                await _redisService.SetDataAsync(cacheKey, ratingsProgress);

                await Clients.Caller.ReceiveAllRatings(ratingsProgress);
            }
            else
            {
                await Clients.Caller.ReceiveAllRatings(cacheRatings);
            }
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var stringConnection = await _redisService.GetDataAsync<string>(Context.ConnectionId);
            var connection = JsonSerializer.Deserialize<UserConnection>(stringConnection);

            if (connection is not null)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.SessionId.ToString() + "-ratings");
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
