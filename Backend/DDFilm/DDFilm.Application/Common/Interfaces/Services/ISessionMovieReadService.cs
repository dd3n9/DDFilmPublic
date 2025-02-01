using DDFilm.Application.DTO;
using DDFilm.Contracts.Common;


namespace DDFilm.Application.Common.Interfaces.Services
{
    public interface ISessionMovieReadService
    {
        Task<IEnumerable<SessionMovieDto>> GetMoviesBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken);
        Task<PaginatedList<SessionMovieDto>> GetWatchedMoviesBySessionIdAsync(PaginationParams paginationParams, Guid sessionId, CancellationToken cancellationToken);
        Task<PaginatedList<SessionMovieDto>> GetUnwatchedMoviesBySessionIdAsync(PaginationParams paginationParams, Guid sessionId, CancellationToken cancellationToken);
        Task<IEnumerable<SessionMovieDto>> GetUnwatchedMoviesBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken);
        Task<SessionMovieDto> GetWatchingMovieBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken);
        Task<bool> IsCurrentlyWatchingMovie(Guid sessionId, CancellationToken cancellationToken);
    }
}
