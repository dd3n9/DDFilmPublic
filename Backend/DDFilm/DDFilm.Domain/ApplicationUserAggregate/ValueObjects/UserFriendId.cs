using DDFilm.Domain.Common.Exceptions;

namespace DDFilm.Domain.ApplicationUserAggregate.ValueObjects
{
    public record UserFriendId
    {
        public Guid Value { get; }

        public UserFriendId(Guid value)
        {
            if(value ==  Guid.Empty)
            {
                throw new EmptyUserFriendIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(UserFriendId id)
            => id.Value;

        public static implicit operator UserFriendId(Guid id)
            => new(id);
    }
}
