using DDFilm.Application.Common.Clients;
using DDFilm.Application.Common.Interfaces.Caching;
using DDFilm.Contracts.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace DDFilm.Infrastructure.Hubs
{
    public class SessionHub : Hub<ISessionClient>
    {
        private readonly IRedisCacheService _redisService;

        public SessionHub(IRedisCacheService redisService)
        {
            _redisService = redisService;
        }

        public async Task JoinSession(UserConnection connection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.SessionId.ToString());

            var stringConnection = JsonSerializer.Serialize(connection);

            await _redisService.SetDataAsync(Context.ConnectionId, stringConnection);

            await Clients.Groups(connection.SessionId.ToString()).UserJoined(connection.UserName);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var stringConnection = await _redisService.GetDataAsync<string>(Context.ConnectionId);
            var connection = JsonSerializer.Deserialize<UserConnection>(stringConnection);

            if(connection is not null)
            {
                await _redisService.RemoveDataAsync(Context.ConnectionId);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.SessionId.ToString());
                await Clients.Group(connection.SessionId.ToString()).UserLeft(connection.UserName);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
