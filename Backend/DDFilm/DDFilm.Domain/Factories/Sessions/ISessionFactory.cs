using DDFilm.Domain.ApplicationUserAggregate;
using DDFilm.Domain.SessionAggregate;
using DDFilm.Domain.SessionAggregate.ValueObjects;

namespace DDFilm.Domain.Factories.Sessions
{
    public interface ISessionFactory
    {
        Task<Session> Create(ApplicationUser user, SessionName sessionName, HashedPassword password);
    }
}
