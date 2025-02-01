using FluentValidation;

namespace DDFilm.Application.SessionMovies.Queries.GetSessionMovies
{
    public sealed class GetSessionMoviesQueryValidator : AbstractValidator<GetSessionMoviesQuery>
    {
        public GetSessionMoviesQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(q => q.SessionId)
                .NotEmpty().WithMessage("Session ID is required.");
        }
    }
}
