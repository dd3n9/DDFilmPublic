using DDFilm.Application.Common.Interfaces.Authentication;
using FluentResults;
using MediatR;

namespace DDFilm.Application.Authentication.Commands.SeedRoles
{
    public class SeedRolesCommandHandler : IRequestHandler<SeedRolesCommand, Result>
    {
        private readonly IUserRoleService _userRoleService;

        public SeedRolesCommandHandler(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        public async Task<Result> Handle(SeedRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRoleService.SeedRolesAsync();

            return result;
        }
    }
}
