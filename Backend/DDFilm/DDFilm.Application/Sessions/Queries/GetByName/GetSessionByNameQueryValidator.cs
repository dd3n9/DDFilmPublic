using FluentValidation;

namespace DDFilm.Application.Sessions.Queries.GetByName
{
    public sealed class GetSessionByNameQueryValidator : AbstractValidator<GetSessionByNameQuery>
    {
        public GetSessionByNameQueryValidator()
        {
            RuleFor(q => q.SessionName)
                .NotEmpty().WithMessage("Session name is required.");
        }
    }
}
