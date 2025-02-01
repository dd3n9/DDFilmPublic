using DDFilm.Domain.Common.Exceptions;

namespace DDFilm.Domain.SessionAggregate.ValueObjects
{
    public record SessionSettingId 
    {
        public  Guid Value { get; }

        public SessionSettingId(Guid value)
        {
            if (value == Guid.Empty)
                throw new EmptySessionSettingIdException();

            Value = value;
        }

        public static SessionSettingId CreateUnique()
        {
            return new (Guid.NewGuid());
        }

        public static implicit operator Guid(SessionSettingId id)
            => id.Value;

        public static implicit operator SessionSettingId(Guid id)
            => new(id);
    }
}
