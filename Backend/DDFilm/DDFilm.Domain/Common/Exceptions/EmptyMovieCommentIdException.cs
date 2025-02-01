using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace DDFilm.Domain.Common.Exceptions
{
    public class EmptyMovieCommentIdException : DDFilmException
    {
        public EmptyMovieCommentIdException()
            : base("Movie Comment ID cannot be empty.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
