namespace DDFilm.Contracts.Sessions
{
    public record CreateSessionRequest(
        string SessionName, 
        string Password
        );
}
