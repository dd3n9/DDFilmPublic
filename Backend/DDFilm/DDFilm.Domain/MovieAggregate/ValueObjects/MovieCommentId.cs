using DDFilm.Domain.Common.Exceptions;

namespace DDFilm.Domain.MovieAggregate.ValueObjects
{
    public record MovieCommentId 
    {
        public  Guid Value { get; }

        public MovieCommentId(Guid value)
        {
            if (value == Guid.Empty)
                throw new EmptyMovieCommentIdException();

            Value = value;
        }

        public static implicit operator Guid(MovieCommentId movieCommentId) 
            => movieCommentId.Value;

        public static implicit operator MovieCommentId(Guid id)
            => new(id);
    }
}
