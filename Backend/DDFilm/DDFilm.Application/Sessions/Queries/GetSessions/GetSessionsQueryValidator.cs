using DDFilm.Application.Common.Validations;
using DDFilm.Application.Sessions.Queries.Sessions;
using FluentValidation;

namespace DDFilm.Application.Sessions.Queries.GetSessions
{
    public sealed class GetSessionsQueryValidator : AbstractValidator<GetSessionsQuery>
    {
        public GetSessionsQueryValidator()
        {
            RuleFor(q => q.PaginationParams)
                .NotNull().WithMessage("Pagination parameters are required.")
                .SetValidator(new PaginationParamsValidator());
        }
    }
}
