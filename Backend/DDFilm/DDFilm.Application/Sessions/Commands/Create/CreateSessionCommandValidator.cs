using FluentValidation;

namespace DDFilm.Application.Sessions.Commands.Create
{
    public sealed class CreateSessionCommandValidator : AbstractValidator<CreateSessionCommand>
    {
        public CreateSessionCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(c => c.SessionName)
                .NotEmpty().WithMessage("Session name is required.")
                .MinimumLength(3).WithMessage("Session name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Session name cannot exceed 100 characters.");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .MaximumLength(50).WithMessage("Password cannot exceed 50 characters.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one number");
        }
    }
}
