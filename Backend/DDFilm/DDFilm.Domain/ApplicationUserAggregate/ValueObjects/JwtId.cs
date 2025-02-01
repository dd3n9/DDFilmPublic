using DDFilm.Domain.Common.Exceptions;

namespace DDFilm.Domain.ApplicationUserAggregate.ValueObjects
{
    public record JwtId
    {
        public string Value { get; }

        public JwtId(string value)
        {
            if (value is null)
                throw new EmptyJwtIdException();

            Value = value;
        }

        public static implicit operator JwtId(string jwtId) 
            => new JwtId(jwtId);

        public static implicit operator string(JwtId jwtId)
            => jwtId.Value;
    }
}
