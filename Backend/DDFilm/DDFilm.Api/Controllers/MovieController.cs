using DDFilm.Api.Extensions;
using DDFilm.Application.Movies.Commands.RateMovie;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDFilm.Api.Controllers
{
    [Route("api/movies")]
    public class MovieController : BaseController
    {
        private readonly ISender _mediator;

        public MovieController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{tmdbId:int}")]
        public async Task<IActionResult> RateMovie([FromRoute] int tmdbId, [FromBody] int rating, CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserIdClaimValue();

            var command = new RateMovieCommand(tmdbId, userId, rating);
            var result = await _mediator.Send(command, cancellationToken);

            return OkOrNotFound(result);
        }
    }
}
