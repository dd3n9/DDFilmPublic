namespace DDFilm.Infrastructure.EF.Models
{
    internal class MovieRatingReadModel : BaseReadModel
    {
        public MovieReadModel Movie { get; set; }
        public Guid MovieId { get; set; }

        public ApplicationUserReadModel User { get; set; }
        public string UserId { get; set; }

        public SessionReadModel Session { get; set; }
        public Guid? SessionId { get; set; }

        public int? Rating { get; set; }
    }
}
