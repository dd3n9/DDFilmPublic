using DDFilm.Contracts.Movies.Responses;

namespace DDFilm.Contracts.SessionMovies.Responses
{
    public class SessionMovieResponse
    {
        public Guid SessionMovieId { get; set; }
        public int TmdbId { get; set; }
        public string SessionName { get; set; }
        public string MovieTitle { get; set; }
        public double? AverageRating { get; set; }
        public string AddedByUserName { get; set; }
        public bool IsWatched { get; set; }
        public bool IsWatching { get; set; }
        public IEnumerable<MovieRatingResponse>? Ratings { get; set; }
        public DateTime? WatchedAt { get; set; } 
    }
}
