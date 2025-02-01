namespace DDFilm.Contracts.Sessions.Responses
{
    public class SessionResponse
    {
        public Guid Id { get; set; }
        public string SessionName { get; set; }
        public int ParticipantLimit { get; set; }
        public int ParticipantsCount { get; set; }
        public string OwnerName { get; set; }
    }
}
