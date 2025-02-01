using DDFilm.Domain.Common.Exceptions;

namespace DDFilm.Domain.SessionAggregate.ValueObjects
{
    public  record SessionId 
    {
        public Guid Value { get; } 

        public SessionId(Guid value)
        {
            if(value == Guid.Empty)
            {
                throw new EmptySessionIdException();
            }
            Value = value;
        }

        public static SessionId Create(Guid value)
        {
            return new SessionId(value);
        }

        public static SessionId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static implicit operator Guid(SessionId id) 
            => id.Value;

        public static implicit operator SessionId(Guid id)
            =>  new(id);
    }
}
