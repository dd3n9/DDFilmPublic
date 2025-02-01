using DDFilm.Domain.Common.Exceptions;

namespace DDFilm.Domain.MovieAggregate.ValueObjects
{
    public record MovieRatingId 
    {
        public Guid Value { get; }

        public MovieRatingId(Guid value)
        {
            if (value == Guid.Empty)
                throw new EmptyMovieRatingIdException();

            Value = value;
        }

        public static MovieRatingId CreateUnique()
        {
            return new MovieRatingId(Guid.NewGuid());
        }

        public static implicit operator Guid(MovieRatingId movieRatingId) 
            => movieRatingId.Value;

        public static implicit operator MovieRatingId(Guid id)
            => new MovieRatingId(id);
    }
}
