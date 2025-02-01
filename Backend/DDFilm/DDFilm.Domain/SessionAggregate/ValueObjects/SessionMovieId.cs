using DDFilm.Domain.Common.Exceptions;

namespace DDFilm.Domain.SessionAggregate.ValueObjects
{
    public record SessionMovieId
    {
        public  Guid Value { get; }

        public SessionMovieId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptySessionMovieIdException();
            }
            Value = value;
        }

        public static SessionMovieId CreateUnique()
        {
            return new SessionMovieId(Guid.NewGuid());
        }

        public static implicit operator Guid(SessionMovieId id)
            => id.Value;

        public static implicit operator SessionMovieId(Guid id)
            => new(id);
    }
}
