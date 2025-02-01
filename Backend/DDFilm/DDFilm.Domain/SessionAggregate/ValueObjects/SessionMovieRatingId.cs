
namespace DDFilm.Domain.SessionAggregate.ValueObjects
{
    public record SessionMovieRatingId 
    {
        public Guid Value { get; }

        public SessionMovieRatingId(Guid value)
        {
            if (value == Guid.Empty)
                throw new Exception();

            Value = value;
        }

        public static implicit operator Guid(SessionMovieRatingId value) 
            => value.Value;

        public static implicit operator SessionMovieRatingId(Guid value)
            => new(value);
    }
}
