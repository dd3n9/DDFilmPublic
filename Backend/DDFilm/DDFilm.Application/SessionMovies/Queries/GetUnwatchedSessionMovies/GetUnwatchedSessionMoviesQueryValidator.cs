using DDFilm.Application.Common.Validations;
using FluentValidation;

namespace DDFilm.Application.SessionMovies.Queries.GetUnwatchedSessionMovies
{
    public sealed class GetUnwatchedSessionMoviesQueryValidator : AbstractValidator<GetUnwatchedSessionMoviesQuery>
    {
        public GetUnwatchedSessionMoviesQueryValidator()
        {
            RuleFor(q => q.PaginationParams)
                .NotNull().WithMessage("Pagination parameters are required.")
                .SetValidator(new PaginationParamsValidator());

            RuleFor(q => q.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(q => q.SessionId)
                .NotEmpty().WithMessage("Session ID is required.");
        }
    }
}
