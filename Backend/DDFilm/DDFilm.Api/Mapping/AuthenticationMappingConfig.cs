using DDFilm.Application.Authentication.Commands.Register;
using DDFilm.Application.Authentication.Queries.Login;
using DDFilm.Contracts.Authentication;
using Mapster;

namespace DDFilm.Api.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();

            config.NewConfig<LoginRequest, LoginQuery>();
        }
    }
}
