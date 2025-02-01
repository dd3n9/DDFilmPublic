using DDFilm.Application.Common.Clients;
using DDFilm.Application.Common.Interfaces.Caching;
using DDFilm.Contracts.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Text.Json;


namespace DDFilm.Infrastructure.Hubs
{
    public class WatchingHub : Hub<IWatchingClient>
    {
        private readonly IHubContext<SessionHub, ISessionClient> _sessionHubContext;
        private readonly IRedisCacheService _redisService;

        public WatchingHub(IHubContext<SessionHub, ISessionClient> sessionHubContext, IRedisCacheService redisService)
        {
            _sessionHubContext = sessionHubContext;
            _redisService = redisService;
        }

        public async Task JoinWatchingSession(UserConnection connection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"{connection.SessionId}-watching");

            string roomName = connection.SessionId.ToString();
            var videoRoomKey = GetVideoRoomKey(roomName);
            var roomState = await _redisService.GetDataAsync<RoomState>(videoRoomKey);

            if(roomState == null)
            {
                roomState = new RoomState
                {
                    LecturerId = null
                };
            }

                await Groups.AddToGroupAsync(Context.ConnectionId, roomName);

                roomState.Viewers.TryAdd(Context.ConnectionId, true);
                await _redisService.SetDataAsync(videoRoomKey, roomState);

            if (roomState.LecturerId != null)
            {
                await Clients.Group($"{connection.SessionId}-watching").ViewerJoined(Context.ConnectionId);

                await Clients.Client(roomState.LecturerId).NewViewerJoined(Context.ConnectionId);
            }
        }

        public async Task LeaveWatchingSession(UserConnection connection)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"{connection.SessionId}-watching");
            await LeaveVideoRoomInternal(connection.SessionId.ToString(), Context.ConnectionId);
        }

        public async Task SendMessage(string message, UserConnection connection)
        {
            var test = Clients.Group($"{connection.SessionId}-watching");
            await Clients.Group($"{connection.SessionId}-watching").ReceiveMessage(connection.UserName, message);
        }

        public async Task StartDemo(UserConnection connection)
        {
            string roomName = connection.SessionId.ToString();
            var videoRoomKey = GetVideoRoomKey(roomName);
            var roomState = await _redisService.GetDataAsync<RoomState>(videoRoomKey);

            if (roomState != null)
            {
                roomState.LecturerId = Context.ConnectionId;
                roomState.Viewers.TryRemove(Context.ConnectionId, out _);
                await _redisService.SetDataAsync(videoRoomKey, roomState);
               
                string[] initialViewerIds = roomState.Viewers.Keys.ToArray();
               
                await Clients.Caller.InitialViewersList(initialViewerIds);

                await _sessionHubContext.Clients.Group(connection.SessionId.ToString()).BroadcastStarted();
               
                await Clients.Group($"{connection.SessionId}-watching").BroadcastStarted(Context.ConnectionId, connection.UserName);
            }
        }

        public async Task EndDemo(UserConnection connection)
        {
            var videoRoomKey = GetVideoRoomKey(connection.SessionId.ToString());
            var roomState = await _redisService.GetDataAsync<RoomState>(videoRoomKey);

            if(roomState != null && roomState.LecturerId == Context.ConnectionId) 
            {
                roomState.LecturerId = null;
                await _redisService.SetDataAsync(videoRoomKey, roomState);

                await Clients.Group($"{connection.SessionId}-watching").BroadcastEnded();
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;
            var stringConnection = await _redisService.GetDataAsync<string>(connectionId);

            await _redisService.RemoveDataAsync(connectionId);

            var connection = JsonSerializer.Deserialize<UserConnection>(stringConnection);

            if (connection != null)
            {
                await LeaveVideoRoomInternal(connection.SessionId.ToString(), connectionId);
                await Groups.RemoveFromGroupAsync(connectionId, $"{connection.SessionId}-watching");
            }

            await base.OnDisconnectedAsync(exception);
        }

        private async Task LeaveVideoRoomInternal(string roomName, string connectionId)
        {
            var videoRoomKey = GetVideoRoomKey(roomName);
            var roomState = await _redisService.GetDataAsync<RoomState>(videoRoomKey);

            if (roomState != null)
            {
                if (roomState.LecturerId == connectionId)
                {
                    await _redisService.RemoveDataAsync(videoRoomKey);
                    await Clients.Group(roomName).BroadcastEnded(); 
                }
                else
                {
                    roomState.Viewers.TryRemove(connectionId, out _);
                    await _redisService.SetDataAsync(videoRoomKey, roomState);
                }
                await Groups.RemoveFromGroupAsync(connectionId, roomName);
            }
        }

        public async Task SendOffer(string targetPeerId, string sdp)
        {
            await Clients.Client(targetPeerId).ReceiveOffer(Context.ConnectionId, sdp);
        }

        public async Task SendAnswer(string targetPeerId, string sdp)
        {
            await Clients.Client(targetPeerId).ReceiveAnswer(Context.ConnectionId, sdp);
        }

        public async Task SendIceCandidate(string targetPeerId, object iceCandidate)
        {
            await Clients.Client(targetPeerId).ReceiveIceCandidate(Context.ConnectionId, iceCandidate);
        }

        private string GetVideoRoomKey(string roomName) => $"VideoRoom:{roomName}";
    }
}
