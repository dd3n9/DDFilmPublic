
namespace DDFilm.Domain.Common.Models
{
    public abstract class DDFilmException : Exception
    {
        public int StatusCode { get; }

        protected DDFilmException(string message, int statusCode) : base(message) 
        {
            StatusCode = statusCode;    
        }
    }
}
