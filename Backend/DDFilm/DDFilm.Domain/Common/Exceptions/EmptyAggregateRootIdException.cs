using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DDFilm.Domain.Common.Exceptions
{
    public class EmptyAggregateRootIdException : DDFilmException
    {
        public EmptyAggregateRootIdException()
            : base("Application User Id cannot be empty", StatusCodes.Status400BadRequest)
        {
        }
    }
}
