using DDFilm.Domain.Common.Exceptions;

namespace DDFilm.Domain.SessionAggregate.ValueObjects
{
    public record HashedPassword
    {
        public string Value { get; }

        public HashedPassword(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new EmptyHashedPasswordException();

            Value = value;
        }

        public static implicit operator HashedPassword(string value) 
            => new HashedPassword(value);

        public static implicit operator string(HashedPassword value)
            => value.Value;
    }
}
