using DDFilm.Application.DTO;
using DDFilm.Contracts.Movies.Responses;
using Mapster;

namespace DDFilm.Api.Mapping
{
    public class MovieMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<MovieRatingDto, MovieRatingResponse>()
                .Map(dest => dest.UserName, src => src.UserName)
                .Map(dest => dest.Rating, src => src.Rating);
        }
    }
}
