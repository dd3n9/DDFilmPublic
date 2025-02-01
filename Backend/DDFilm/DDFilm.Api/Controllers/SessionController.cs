using DDFilm.Api.Extensions;
using DDFilm.Application.Sessions.Commands.Create;
using DDFilm.Application.Sessions.Commands.Delete;
using DDFilm.Application.Sessions.Commands.Login;
using DDFilm.Application.Sessions.Commands.Logout;
using DDFilm.Application.Sessions.Queries.GetByName;
using DDFilm.Application.Sessions.Queries.GetMySessions;
using DDFilm.Application.Sessions.Queries.Sessions;
using DDFilm.Contracts.Common;
using DDFilm.Contracts.Configurations;
using DDFilm.Contracts.Sessions;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDFilm.Api.Controllers
{
    [Route("api/sessions")]
    public class SessionController : BaseController
    {
        public readonly ISender _mediator;
        public readonly IMapper _mapper;

        public SessionController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{sessionName}")]
        public async Task<ActionResult> GetByNameAsync([FromRoute] string sessionName, CancellationToken cancellationToken)
        {
            var command = new GetSessionByNameQuery(sessionName);

            var result = await _mediator.Send(command, cancellationToken);
            return OkOrNotFound(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetSessions([FromQuery] int pageSize, [FromQuery] int pageNumber, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetSessionsQuery(
                new PaginationParams
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                }), cancellationToken);

            return OkOrNotFound(result);
        }

        [HttpGet("my-sessions")]
        public async Task<ActionResult> GetMySessions([FromQuery] int pageSize, [FromQuery] int pageNumber, CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserIdClaimValue();
            var paginationParams = new PaginationParams 
            { 
                PageNumber = pageNumber, 
                PageSize = pageSize 
            };

            var command = await _mediator.Send(new GetMySessionsQuery(paginationParams, userId), cancellationToken);

            return OkOrNotFound(command);
        }

        [HttpPost("{sessionId:guid}/login")]
        public async Task<IActionResult> Login([FromRoute] Guid sessionId, [FromBody] SessionLoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserIdClaimValue();
            var command = new LoginSessionCommand(sessionId, userId, loginRequest.Password);

            var result = await _mediator.Send(command, cancellationToken);

            return OkOrNotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSessionRequest request, CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserIdClaimValue();
            var command = new CreateSessionCommand(userId, request.SessionName, request.Password);

            var result = await _mediator.Send(command, cancellationToken);

            return OkOrNotFound(result);
        }

        [HttpDelete("{sessionId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid sessionId, CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserIdClaimValue();
            var command = new DeleteSessionCommand(userId, sessionId);

            var result = await _mediator.Send(command, cancellationToken);

            return OkOrNotFound(result);
        }

        [HttpDelete("{sessionId:guid}/logout")]
        public async Task<IActionResult> Logout([FromRoute] Guid sessionId, CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserIdClaimValue();
            var command = new LogoutSessionCommand(sessionId, userId);

            var result = await _mediator.Send(command, cancellationToken);
            return OkOrNotFound(result);
        }
    }
}
