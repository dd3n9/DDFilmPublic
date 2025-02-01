namespace DDFilm.Application.DTO
{
    public class SessionMovieDto
    {
        public Guid SessionMovieId { get; set; }
        public Guid SessionId { get; set; }
        public Guid MovieId { get; set; }
        public int TmdbId { get; set; }
        public string SessionName { get; set; }
        public string MovieTitle { get; set; }
        public string AddedByUserId { get; set; }
        public string AddedByUserName { get; set; }
        public bool IsWatched { get; set; }
        public double? AverageRating { get; set; }
        public IEnumerable<MovieRatingDto>? Ratings { get; set; }
        public DateTime? WatchedAt { get; set; }
    }
}
