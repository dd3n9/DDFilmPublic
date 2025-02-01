using DDFilm.Application.DTO;

namespace DDFilm.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(AuthenticationDto authenticationDto);
    }
}
