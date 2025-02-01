using FluentValidation;

namespace DDFilm.Application.Sessions.Commands.Login
{
    public sealed class LoginSessionCommandValidator : AbstractValidator<LoginSessionCommand>
    {
        public LoginSessionCommandValidator()
        {
            RuleFor(c => c.SessionId)
                .NotEmpty().WithMessage("Session ID is required.");

            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("User ID is required()");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
