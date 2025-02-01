using DDFilm.Application.Common.Interfaces.Authentication;
using DDFilm.Domain.ApplicationUserAggregate;
using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.Common.Constants;
using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace DDFilm.Infrastructure.Authentication
{
    public class UserRoleService : IUserRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<ApplicationUserId>> _roleManager;

        public UserRoleService(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole<ApplicationUserId>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task AssignRoleAsync(ApplicationUser applicationUser, string userRole)
        {
            if(await _userManager.IsInRoleAsync(applicationUser, userRole))
            {
                return;
            }

            await _userManager.AddToRoleAsync(applicationUser, userRole);
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(ApplicationUserId userId)
        {
            var user = await _userManager.FindByIdAsync(userId.Value);
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<Result> SeedRolesAsync()
        {
            bool isOwnerRoleExists = await _roleManager.RoleExistsAsync(StaticApplicationUserRoles.OWNER);
            bool isPremiumRoleExists = await _roleManager.RoleExistsAsync(StaticApplicationUserRoles.PREMIUM);
            bool isUserRoleExists = await _roleManager.RoleExistsAsync(StaticApplicationUserRoles.USER);

            if (isOwnerRoleExists && isPremiumRoleExists && isUserRoleExists)
                return Result.Ok();

            await _roleManager.CreateAsync(new IdentityRole<ApplicationUserId>
            {
                Id = ApplicationUserId.CreateUnique(),
                Name = StaticApplicationUserRoles.OWNER,
                NormalizedName = StaticApplicationUserRoles.OWNER.ToUpper()
            });

            await _roleManager.CreateAsync(new IdentityRole<ApplicationUserId>
            {
                Id = ApplicationUserId.CreateUnique(), 
                Name = StaticApplicationUserRoles.PREMIUM,
                NormalizedName = StaticApplicationUserRoles.PREMIUM.ToUpper()
            });

            await _roleManager.CreateAsync(new IdentityRole<ApplicationUserId>
            {
                Id = ApplicationUserId.CreateUnique(), 
                Name = StaticApplicationUserRoles.USER,
                NormalizedName = StaticApplicationUserRoles.USER.ToUpper()
            });

            return Result.Ok();
        }
    }
}
