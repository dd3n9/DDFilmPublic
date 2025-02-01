using DDFilm.Application.DTO;

namespace DDFilm.Application.Common.Interfaces.Services
{
    public interface IApplicationUserReadService
    {
        Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
        Task<ApplicationUserDto> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
