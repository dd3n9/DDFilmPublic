namespace DDFilm.Application.DTO
{
    public class MovieRatingDto
    {
        public Guid MovieRatingId { get; set; }
        public Guid? SessionId { get; set; }
        public Guid MovieId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int? Rating { get; set; }
    }
}
