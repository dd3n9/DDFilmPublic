namespace DDFilm.Application.DTO
{
    public class SessionDto
    {
        public Guid Id { get; set; }
        public string SessionName { get; set; }
        public string OwnerId { get; set; }
        public string OwnerName { get; set; }
        public SessionSettingsDto Settings { get; set; }
        public IEnumerable<SessionParticipantDto> Participants { get; set; }
    }
}
