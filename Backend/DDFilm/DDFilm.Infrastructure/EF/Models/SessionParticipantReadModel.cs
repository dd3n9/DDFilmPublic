
namespace DDFilm.Infrastructure.EF.Models
{
    internal class SessionParticipantReadModel : BaseReadModel
    {
        public SessionReadModel Session{ get; set; }
        public Guid SessionId { get; set; }
        public ApplicationUserReadModel ApplicationUser {  get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}
