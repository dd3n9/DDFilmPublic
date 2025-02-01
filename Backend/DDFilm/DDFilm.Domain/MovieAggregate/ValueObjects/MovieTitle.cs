using DDFilm.Domain.Common.Exceptions;


namespace DDFilm.Domain.MovieAggregate.ValueObjects
{
    public record MovieTitle 
    {
        public string Value { get; }

        public MovieTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyMovieTitleException();
            Value = value;
        }

        public static implicit operator MovieTitle(string value) 
            => new(value);

        public static implicit operator string(MovieTitle value)
            => value.Value; 
    }
}
