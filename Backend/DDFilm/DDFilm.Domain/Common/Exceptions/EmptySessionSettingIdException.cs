using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class EmptySessionSettingIdException : DDFilmException
    {
        public EmptySessionSettingIdException()
            : base("Session Setting ID cannot be empty.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
