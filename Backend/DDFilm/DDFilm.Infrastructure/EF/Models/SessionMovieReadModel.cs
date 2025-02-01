
namespace DDFilm.Infrastructure.EF.Models
{
    internal class SessionMovieReadModel : BaseReadModel
    {
        public MovieReadModel Movie{ get; set; }
        public Guid MovieId { get; set; }
        public SessionReadModel Session { get; set; }
        public Guid SessionId { get; set; }
        public ApplicationUserReadModel ApplicationUser { get; set; }
        public string AddedByUserId { get; set; }
        public bool IsWatched { get; set; }
    }
}
