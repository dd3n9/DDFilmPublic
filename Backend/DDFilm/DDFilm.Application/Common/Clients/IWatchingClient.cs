namespace DDFilm.Application.Common.Clients
{
    public interface IWatchingClient
    {
        Task ReceiveMessage(string user, string message);
        Task BroadcastStarted(string streamerConnectionId, string streamerUserName);
        Task BroadcastEnded();
        Task ReceiveOffer(string peerId, string sdp);
        Task ReceiveAnswer(string peerId, string sdp);
        Task ReceiveIceCandidate(string peerId, object iceCandidate);
        Task InitialViewersList(string[] viewerIds);
        Task ViewerJoined(string viewerId);
        Task NewViewerJoined(string viewerId);
    }
}
