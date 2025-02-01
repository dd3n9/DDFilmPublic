using DDFilm.Domain.ApplicationUserAggregate;
using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.Common.Errors;
using DDFilm.Domain.Repositories;
using DDFilm.Infrastructure.EF.Context;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DDFilm.Infrastructure.EF.Repositories
{
    internal sealed class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly DbSet<ApplicationUser> _user;
        private readonly WriteDbContext _writeDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserRepository(WriteDbContext writeContext,
            UserManager<ApplicationUser> userManager)
        {
            _user = writeContext.Users;
            _writeDbContext = writeContext;
            _userManager = userManager;
        }

        public async Task<Result> AddAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
        {
            var result = await _userManager.CreateAsync(applicationUser);
            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .Select(e => ApplicationErrors.ApplicationUser.CustomValidationError(e.Description))
                    .ToList();

                return Result.Fail(errors);
            }

            return Result.Ok();
        }

        public async Task<ApplicationUser> GetByIdAsync(ApplicationUserId id, CancellationToken cancellationToken)
        {
            return await _user.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
        {
            _user.Update(applicationUser);
            await _writeDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
