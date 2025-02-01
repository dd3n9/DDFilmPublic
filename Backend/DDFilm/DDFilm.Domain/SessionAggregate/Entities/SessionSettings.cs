using DDFilm.Domain.Common.Exceptions;
using DDFilm.Domain.Common.Models;
using DDFilm.Domain.SessionAggregate.ValueObjects;

namespace DDFilm.Domain.SessionAggregate.Entities
{
    public class SessionSettings : Entity<SessionSettingId>
    {
        public uint ParticipantLimit { get; private set; }
        public uint RequiredMoviesPerUser { get; private set; }

        private SessionSettings(SessionSettingId sessionSettingId, uint participantLimit, uint requiredMoviesPerUser)
            : base(sessionSettingId)
        {
            if (participantLimit < 1 || participantLimit > 10)
                throw new ParticipantLimitException();

            if (requiredMoviesPerUser < 1 || requiredMoviesPerUser > 5)
                throw new RequiredMoviesNumberPerUserException();

            ParticipantLimit = participantLimit;
            RequiredMoviesPerUser = requiredMoviesPerUser;
        }

        private SessionSettings() { }

        private SessionSettings(
            uint participantLimit,
            uint requiredMoviesPerUser)
        {
            if (participantLimit < 1 || participantLimit > 10)
                throw new ParticipantLimitException();

            if (requiredMoviesPerUser < 1 || requiredMoviesPerUser > 5)
                throw new RequiredMoviesNumberPerUserException();
        }

        public static SessionSettings Create(
            uint participantLimit,
            uint requiredMoviesPerUser
            )
        {
            var settings = new SessionSettings(
                SessionSettingId.CreateUnique(),
                participantLimit,
                requiredMoviesPerUser);

            return settings;
        }
    }
}
