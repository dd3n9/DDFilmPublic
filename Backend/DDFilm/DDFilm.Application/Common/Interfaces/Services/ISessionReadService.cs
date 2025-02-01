using DDFilm.Application.DTO;
using DDFilm.Contracts.Common;

namespace DDFilm.Application.Common.Interfaces.Services
{
    public interface ISessionReadService
    {
        Task<IEnumerable<SessionDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<PaginatedList<SessionDto>> GetAllSortedAsync(PaginationParams paginationParams, CancellationToken cancellationToken);
        Task<SessionDto> GetByNameAsync(string name, CancellationToken cancellationToken);
        Task<IEnumerable<SessionDto>> GetByUserIdAsync(string userId, CancellationToken cancellationToken);
        Task<PaginatedList<SessionDto>> GetByUserIdSortedAsync(PaginationParams paginationParams, string userId, CancellationToken cancellationToken);
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
        Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
