using DDFilm.Application.DTO;
using DDFilm.Application.Sessions.Queries.Sessions;
using DDFilm.Contracts.Common;
using DDFilm.Contracts.Sessions;
using DDFilm.Contracts.Sessions.Responses;
using Mapster;

namespace DDFilm.Api.Mapping
{
    public class SessionMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetSessionsRequest, GetSessionsQuery>();

            config.NewConfig<SessionDto, SessionResponse>()
                .Map(dest => dest.SessionName, src => src.SessionName)
                .Map(dest => dest.ParticipantsCount, src => src.Participants.Count())
                .Map(dest => dest.ParticipantLimit, src => src.Settings.ParticipantLimit)
                .Map(dest => dest.OwnerName, src => src.OwnerName);

            config.NewConfig<PaginatedList<SessionDto>, PaginatedList<SessionResponse>>()
                .ConstructUsing(src => new PaginatedList<SessionResponse>(
                    src.Items.Adapt<List<SessionResponse>>(), src.TotalCount, src.CurrentPage, src.PageSize))
                .Map(dest => dest.TotalCount, src => src.TotalCount)
                .Map(dest => dest.CurrentPage, src => src.CurrentPage)
                .Map(dest => dest.PageSize, src => src.PageSize);
        }
    }
}
