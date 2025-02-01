using DDFilm.Application.DTO;

namespace DDFilm.Application.Common.Interfaces.Services
{
    public interface IMovieRatingReadService
    {
        Task<IEnumerable<MovieRatingDto>> GetMovieRatingsBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken);
        Task<IEnumerable<MovieRatingDto>> GetMovieRatingsBySessionIdAsync(int TmdbId, Guid sessionId, CancellationToken cancellationToken);
    }
}
