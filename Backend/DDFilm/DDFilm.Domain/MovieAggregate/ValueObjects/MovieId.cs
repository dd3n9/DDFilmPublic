using DDFilm.Domain.Common.Exceptions;

namespace DDFilm.Domain.MovieAggregate.ValueObjects
{
    public record MovieId 
    {
        public  Guid Value { get; }

        public MovieId(Guid value)
        {
            if (value == Guid.Empty)
                throw new EmptyMovieIdException();

            Value = value;
        }

        public static MovieId Create(Guid value)
        {
            return new MovieId(value);
        }

        public static MovieId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static implicit operator Guid(MovieId movieId)
            => movieId.Value;

        public static implicit operator MovieId(Guid movieId)
            => new MovieId(movieId);
    }
}
