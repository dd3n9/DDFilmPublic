using DDFilm.Application.DTO;
using DDFilm.Infrastructure.EF.Models;

namespace DDFilm.Infrastructure.EF.ModelExtensions
{
    internal static class ApplicationUserExtensions
    {
        public static ApplicationUserDto AsDto(this ApplicationUserReadModel model)
            => new()
            {
                Id = model.Id,
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.HashedPassword
            };
    }
}
