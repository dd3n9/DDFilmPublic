namespace DDFilm.Infrastructure.EF.Models
{
    internal class SessionSettingReadModel 
    {
        public Guid Id { get; set; }
        public uint ParticipantLimit { get; set; }
        public uint RequiredMoviesPerUser { get; set; }
    }
}
