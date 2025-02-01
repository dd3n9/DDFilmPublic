using FluentValidation;

namespace DDFilm.Application.Sessions.Commands.Logout
{
    public sealed class LogoutSessionCommandValidator : AbstractValidator<LogoutSessionCommand>
    {
        public LogoutSessionCommandValidator()
        {
            RuleFor(c => c.SessionId)
                .NotEmpty().WithMessage("Session ID is required.");

            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("User ID is required.");
        }
    }
}
