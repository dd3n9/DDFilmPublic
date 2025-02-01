using FluentValidation;

namespace DDFilm.Application.SessionParticipants.Queries.GetSessionParticpants
{
    public sealed class GetSessionParticipantsQueryValidator : AbstractValidator<GetSessionParticipantsQuery>
    {
        public GetSessionParticipantsQueryValidator()
        {
            RuleFor(q => q.SessionId)
                .NotEmpty().WithMessage("Session ID is required.");

            RuleFor(q => q.UserId)
                .NotEmpty().WithMessage("User ID is required.");
        }
    }
}
