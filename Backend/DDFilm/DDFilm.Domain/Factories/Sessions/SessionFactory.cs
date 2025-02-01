using DDFilm.Domain.ApplicationUserAggregate;
using DDFilm.Domain.Policies;
using DDFilm.Domain.SessionAggregate;
using DDFilm.Domain.SessionAggregate.ValueObjects;

namespace DDFilm.Domain.Factories.Sessions
{
    public sealed class SessionFactory : ISessionFactory
    {
        private readonly IEnumerable<ISessionSettingsPolicy> _settingsPolicies;

        public SessionFactory(
            IEnumerable<ISessionSettingsPolicy> settingsPolicies)
        {
            _settingsPolicies = settingsPolicies;
        }

        public async Task<Session> Create(ApplicationUser user, SessionName sessionName, HashedPassword password)
        {
            var applicableSettingsPolicy = await FindApplicablePolicyAsync(user);

            if (applicableSettingsPolicy == null)
                throw new InvalidOperationException("No applicable settings policy found");

            var settings = applicableSettingsPolicy.GetSettings();

            var session = Session.Create(
                sessionName,
                password,
                settings);

            session.AddParticipant(user.Id, SessionRole.Owner);

            return session;
        }

        private async Task<ISessionSettingsPolicy?> FindApplicablePolicyAsync(ApplicationUser user)
        {
            foreach (var policy in _settingsPolicies)
            {
                if (await policy.IsApplicableAsync(user))
                {
                    return policy;
                }
            }
            return null;
        }
    }
}
