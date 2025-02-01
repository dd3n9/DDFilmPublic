using DDFilm.Domain.ApplicationUserAggregate;
using DDFilm.Domain.SessionAggregate.Entities;

namespace DDFilm.Domain.Policies
{
    public interface ISessionSettingsPolicy
    {
        Task<bool> IsApplicableAsync(ApplicationUser user);
        SessionSettings GetSettings();
    }
}
