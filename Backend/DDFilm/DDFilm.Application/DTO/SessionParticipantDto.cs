namespace DDFilm.Application.DTO
{
    public class SessionParticipantDto
    {
        public Guid SessionId { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}
