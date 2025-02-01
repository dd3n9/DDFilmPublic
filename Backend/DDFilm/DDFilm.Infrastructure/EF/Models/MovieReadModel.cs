namespace DDFilm.Infrastructure.EF.Models
{
    internal class MovieReadModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int TmdbId { get; set; }
        public List<MovieRatingReadModel> Ratings { get; set; } = new();
    }
}
