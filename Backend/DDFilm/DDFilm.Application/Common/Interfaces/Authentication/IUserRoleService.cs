using DDFilm.Domain.ApplicationUserAggregate;
using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using FluentResults;

namespace DDFilm.Application.Common.Interfaces.Authentication
{
    public interface IUserRoleService
    {
        Task<Result> SeedRolesAsync();
        Task<IEnumerable<string>> GetUserRolesAsync(ApplicationUserId userId);
        Task AssignRoleAsync(ApplicationUser applicationUser, string userRole);
    }
}
