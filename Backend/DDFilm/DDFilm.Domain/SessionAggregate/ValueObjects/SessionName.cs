using DDFilm.Domain.Common.Exceptions;

namespace DDFilm.Domain.SessionAggregate.ValueObjects
{
    public record SessionName
    {
        public string Value { get; }

        public SessionName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new EmptySessionNameException();
            }
            Value = value;
        }

        public static implicit operator string(SessionName name)
            => name.Value;

        public static implicit operator SessionName(string name)
            => new(name);
    }
}
