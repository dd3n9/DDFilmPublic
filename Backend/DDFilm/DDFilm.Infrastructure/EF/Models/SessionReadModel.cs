namespace DDFilm.Infrastructure.EF.Models
{
    internal class SessionReadModel : BaseReadModel
    {
        public string SessionName { get; set; }
        public string HashedPassword { get; set; }
        public SessionSettingReadModel Settings { get; set; }
        public ICollection<SessionParticipantReadModel> Participants { get; set; }
        public ICollection<SessionMovieReadModel> SessionMovies { get; set; }  
    }
}
