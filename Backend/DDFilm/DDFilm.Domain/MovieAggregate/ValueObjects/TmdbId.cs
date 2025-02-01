using DDFilm.Domain.Common.Exceptions;

namespace DDFilm.Domain.MovieAggregate.ValueObjects
{
    public record TmdbId
    {
        public int Value { get; }

        public TmdbId(int value)
        {
            if (value <= 0)
                throw new EmptyMovieIdException();

            Value = value;
        }

        public static implicit operator int(TmdbId tmdbId)
            => tmdbId.Value;

        public static implicit operator TmdbId(int id)
            => new(id);
    }
}
