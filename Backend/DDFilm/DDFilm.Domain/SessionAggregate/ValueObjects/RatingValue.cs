using DDFilm.Domain.Common.Exceptions;

namespace DDFilm.Domain.SessionAggregate.ValueObjects
{
    public record RatingValue
    {
        public int Value { get; }

        public RatingValue(int value)
        {
            if (value < 1 || value > 5)
                throw new InvalidRatingValueException();

            Value = value;
        }

        public static implicit operator RatingValue(int rating)
            => new(rating);

        public static implicit operator int(RatingValue rating)
            => rating.Value;
    }
}
