using DDFilm.Application.Common.Validations;
using FluentValidation;

namespace DDFilm.Application.Sessions.Queries.GetMySessions
{
    public sealed class GetMySessionsQueryValidator : AbstractValidator<GetMySessionsQuery>
    {
        public GetMySessionsQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(q => q.PaginationParams)
                .NotNull().WithMessage("Pagination parameters are required.")
                .SetValidator(new PaginationParamsValidator());
        }
    }
}
