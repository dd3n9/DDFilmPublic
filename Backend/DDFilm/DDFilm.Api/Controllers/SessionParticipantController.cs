using DDFilm.Api.Extensions;
using DDFilm.Application.SessionParticipants.Queries.GetSessionParticpants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDFilm.Api.Controllers
{
    [Route("api/sessions/{sessionId}/participants")]
    public class SessionParticipantController : BaseController
    {
        private readonly ISender _mediator;

        public SessionParticipantController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetSessionParticipant([FromRoute] Guid sessionId, CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserIdClaimValue();
            var result = await _mediator.Send(new GetSessionParticipantsQuery(sessionId, userId), cancellationToken);

            return OkOrNotFound(result);
        }
    }
}
