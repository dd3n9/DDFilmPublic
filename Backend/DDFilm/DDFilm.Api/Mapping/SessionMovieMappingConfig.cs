using DDFilm.Application.DTO;
using DDFilm.Contracts.Common;
using DDFilm.Contracts.SessionMovies.Responses;
using DDFilm.Contracts.Sessions.Responses;
using DDFilm.Domain.SessionAggregate.Entities;
using Mapster;

namespace DDFilm.Api.Mapping
{
    public class SessionMovieMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<SessionMovieDto, SessionMovieResponse>()
                .Map(dest => dest.SessionMovieId, src => src.SessionMovieId)
                .Map(dest => dest.TmdbId, src => src.TmdbId)
                .Map(dest => dest.SessionName, src => src.SessionName)
                .Map(dest => dest.MovieTitle, src => src.MovieTitle)
                .Map(dest => dest.AverageRating, src => src.AverageRating)
                .Map(dest => dest.AddedByUserName, src => src.AddedByUserName)
                .Map(dest => dest.IsWatched, src => src.IsWatched)
                .Map(dest => dest.IsWatching,
                    src => src.IsWatched && src.Ratings != null && src.Ratings.Any(s => s.Rating == null))
                .Map(dest => dest.Ratings, src => src.Ratings)
                .Map(dest => dest.WatchedAt, src => src.WatchedAt);

            config.NewConfig<SessionMovie, ChooseSessionMovieResponse>()
                .Map(dest => dest.SessionMovieId, src => src.Id);

            config.NewConfig<PaginatedList<SessionMovieDto>, PaginatedList<SessionMovieResponse>>()
                .ConstructUsing(src => new PaginatedList<SessionMovieResponse>(
                    src.Items.Adapt<List<SessionMovieResponse>>(), src.TotalCount, src.CurrentPage, src.PageSize))
                .Map(dest => dest.TotalCount, src => src.TotalCount)
                .Map(dest => dest.CurrentPage, src => src.CurrentPage)
                .Map(dest => dest.PageSize, src => src.PageSize);
        }
    }
}
