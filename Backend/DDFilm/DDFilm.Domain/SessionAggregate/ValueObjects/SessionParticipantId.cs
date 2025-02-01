
namespace DDFilm.Domain.SessionAggregate.ValueObjects
{
    public record SessionParticipantId
    {
        public  Guid Value { get; }

        public SessionParticipantId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentNullException("value");

            Value = value;
        }

        public static SessionParticipantId CreateUnique()
        {
            return new SessionParticipantId(Guid.NewGuid());
        }

        public static implicit operator Guid(SessionParticipantId id) 
            => id.Value;

        public static implicit operator SessionParticipantId(Guid id)
            => new(id);
    }
}
