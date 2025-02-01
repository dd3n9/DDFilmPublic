namespace DDFilm.Application.Common.Clients
{
    public interface ISessionClient
    {
        Task UserJoined(string userName);
        Task UserLeft(string userName);
        Task BroadcastStarted();
        Task RatingGiven(string userName);
    }
}
