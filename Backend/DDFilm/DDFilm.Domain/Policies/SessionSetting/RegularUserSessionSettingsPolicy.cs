using DDFilm.Domain.ApplicationUserAggregate;
using DDFilm.Domain.Common.Constants;
using DDFilm.Domain.SessionAggregate.Entities;
using Microsoft.AspNetCore.Identity;

namespace DDFilm.Domain.Policies.SessionSetting
{
    public class RegularUserSessionSettingsPolicy : ISessionSettingsPolicy
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegularUserSessionSettingsPolicy(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> IsApplicableAsync(ApplicationUser user)
        {
            return await _userManager.IsInRoleAsync(user, StaticApplicationUserRoles.USER);
        }

        public SessionSettings GetSettings()
        {
            return SessionSettings.Create(participantLimit: 3, requiredMoviesPerUser: 3);
        }
    }
}
