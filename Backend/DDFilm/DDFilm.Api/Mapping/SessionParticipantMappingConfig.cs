using DDFilm.Application.DTO;
using DDFilm.Contracts.SessionParticipant.Responses;
using Mapster;

namespace DDFilm.Api.Mapping
{
    public class SessionParticipantMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<SessionParticipantDto, SessionParticipantResponse>()
                .Map(dest => dest.SessionId, src => src.SessionId)
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.UserName, src => src.UserName)
                .Map(dest => dest.Role, src => src.Role);
        }
    }
}
