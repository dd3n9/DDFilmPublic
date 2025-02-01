using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class AddYourselfAsFriendException : DDFilmException
    {
        public AddYourselfAsFriendException() 
            : base("Can`t add yourself as a friend.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
