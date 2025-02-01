using DDFilm.Api.Extensions;
using DDFilm.Application.SessionMovies.Commands.Choose;
using DDFilm.Application.SessionMovies.Commands.Create;
using DDFilm.Application.SessionMovies.Commands.Delete;
using DDFilm.Application.SessionMovies.Queries.GetAllUnwatchedSessionMovies;
using DDFilm.Application.SessionMovies.Queries.GetSessionMovies;
using DDFilm.Application.SessionMovies.Queries.GetUnwatchedSessionMovies;
using DDFilm.Application.SessionMovies.Queries.GetWatchedSessionMovies;
using DDFilm.Application.SessionMovies.Queries.GetWatchingSessionMovie;
using DDFilm.Contracts.Common;
using DDFilm.Contracts.SessionMovies;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace DDFilm.Api.Controllers
{
    [Route("api/sessions/{sessionId}/movies")]
    public class SessionMovieController : BaseController
    {
        private readonly ISender _mediator;

        public SessionMovieController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetSessionMovie([FromRoute] Guid sessionId, CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserIdClaimValue();
            var command = new GetSessionMoviesQuery(userId, sessionId);

            var result = await _mediator.Send(command, cancellationToken);
            return OkOrNotFound(result);
        }

        [HttpGet("watched")]
        public async Task<IActionResult> GetWatchedMovies([FromQuery] int pageSize, [FromQuery] int pageNumber, [FromRoute] Guid sessionId, CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserIdClaimValue();
            var paginationParams = new PaginationParams
            {
                PageSize = pageSize, 
                PageNumber = pageNumber
            };

            var result = await _mediator.Send(new GetWatchedSessionMoviesQuery(paginationParams, userId, sessionId), cancellationToken);

            return OkOrNotFound(result);

        }

        [HttpGet("unwatched")]
        public async Task<IActionResult> GetUnwatchedMovies([FromQuery] int pageSize, [FromQuery] int pageNumber, [FromRoute] Guid sessionId, CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserIdClaimValue();
            var paginationParams = new PaginationParams
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            var result = await _mediator.Send(new GetUnwatchedSessionMoviesQuery(paginationParams, userId, sessionId), cancellationToken);

            return OkOrNotFound(result);

        }

        [HttpGet("all-unwatched")]
        public async Task<IActionResult> GetAllUnwatchedMovies(Guid sessionId, CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserIdClaimValue();

            var result = await _mediator.Send(new GetAllUnwatchedSessionMoviesQuery(userId, sessionId), cancellationToken); 

            return OkOrNotFound(result);
        }

        [HttpGet("watching")]
        public async Task<IActionResult> GetWatchingMovies(Guid sessionId, CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserIdClaimValue();

            var result = await _mediator.Send(new GetWatchingSessionMovieQuery(userId, sessionId), cancellationToken);

            return OkOrNotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSessionMovieRequest request, [FromRoute] Guid sessionId, CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserIdClaimValue();
            var command = new CreateSessionMovieCommand(userId, sessionId, request.TmdbId, request.MovieTitle);

            var result = await _mediator.Send(command, cancellationToken);
            return OkOrNotFound(result);
        }

        [HttpDelete("{tmdbId:int}")]
        public async Task<IActionResult> Delete([FromRoute] Guid sessionId, [FromRoute] int tmdbId, CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserIdClaimValue();
            var command = new DeleteSessionMovieCommand(sessionId, tmdbId, userId);

            var result = await _mediator.Send(command, cancellationToken);
            return OkOrNotFound(result);
        }

        [HttpPost("chooseMovie")]
        public async Task<IActionResult> ChooseMovie([FromRoute] Guid sessionId, CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserIdClaimValue();
            var command = new ChooseSessionMovieCommand(sessionId, userId);

            var result = await _mediator.Send(command, cancellationToken);
            return OkOrNotFound(result);    
        }
    }
}
