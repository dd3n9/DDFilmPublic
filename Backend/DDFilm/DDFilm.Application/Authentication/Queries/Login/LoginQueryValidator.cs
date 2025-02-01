using FluentValidation;

namespace DDFilm.Application.Authentication.Queries.Login
{
    public sealed class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not a valid email address.");

            RuleFor(x => x.Password)
              .NotEmpty().WithMessage("Password is required");
        }
    }
}
