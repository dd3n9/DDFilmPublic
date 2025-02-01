using FluentValidation;

namespace DDFilm.Application.Sessions.Commands.Delete
{
    public sealed class DeleteSessionCommandValidator : AbstractValidator<DeleteSessionCommand>
    {
        public DeleteSessionCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(c => c.SessionId)
                .NotEmpty().WithMessage("Session ID is required.");
        }
    }
}
