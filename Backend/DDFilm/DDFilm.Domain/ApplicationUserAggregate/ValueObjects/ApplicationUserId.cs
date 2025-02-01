using DDFilm.Domain.Common.Exceptions;
using DDFilm.Domain.Common.TypeConverters;
using System.ComponentModel;

namespace DDFilm.Domain.ApplicationUserAggregate.ValueObjects
{
    [TypeConverter(typeof(ApplicationUserIdConverter))]
    public record ApplicationUserId
    {
        public string Value { get; }

        public ApplicationUserId(string value)
        {
            if (value is null)
                throw new EmptyApplicationUserIdException();
            Value = value;
        }

        public static ApplicationUserId CreateUnique()
        {
            return new(Guid.NewGuid().ToString());
        }

        public static implicit operator ApplicationUserId(string id)
            => new ApplicationUserId(id);

        public static implicit operator string(ApplicationUserId value)
            => value.Value;

        public override string ToString()
            => Value;
    }
}
