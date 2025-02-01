using DDFilm.Domain.ApplicationUserAggregate;
using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using FluentResults;

namespace DDFilm.Domain.Repositories
{
    public interface IApplicationUserRepository
    {
        Task<ApplicationUser> GetByIdAsync(ApplicationUserId id, CancellationToken cancellationToken);
        Task<Result> AddAsync(ApplicationUser applicationUser, CancellationToken cancellationToken);
        Task UpdateAsync(ApplicationUser applicationUser, CancellationToken cancellationToken);
    }
}
