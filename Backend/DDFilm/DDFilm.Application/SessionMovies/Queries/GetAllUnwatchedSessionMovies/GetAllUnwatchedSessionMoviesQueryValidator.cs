using FluentValidation;

namespace DDFilm.Application.SessionMovies.Queries.GetAllUnwatchedSessionMovies
{
    public sealed class GetAllUnwatchedSessionMoviesQueryValidator : AbstractValidator<GetAllUnwatchedSessionMoviesQuery>
    {
        public GetAllUnwatchedSessionMoviesQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(q => q.SessionId)
                .NotEmpty().WithMessage("Session ID is required.");
        }
    }
}
