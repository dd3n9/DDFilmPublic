using DDFilm.Application.Common.Interfaces.Services;
using DDFilm.Application.DTO;
using DDFilm.Infrastructure.EF.Context;
using DDFilm.Infrastructure.EF.ModelExtensions;
using DDFilm.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;


namespace DDFilm.Infrastructure.EF.Services
{
    internal sealed class ApplicationUserReadService : IApplicationUserReadService
    {
        private readonly DbSet<ApplicationUserReadModel> _user;

        public ApplicationUserReadService(ReadDbContext readDbContext)
        {
            _user = readDbContext.Users;
        }

        public Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
            => _user.AnyAsync(u =>  u.Email == email, cancellationToken);

        public async Task<ApplicationUserDto> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var userDto = await _user
                .Where(u => u.Email == email)
                .Select(u => u.AsDto())
                .FirstOrDefaultAsync(cancellationToken);

            return userDto;
        }
    }
}
