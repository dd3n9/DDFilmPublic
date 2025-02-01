using FluentValidation;

namespace DDFilm.Application.SessionMovies.Commands.Choose
{
    public sealed class ChooseSessionMovieCommandValidator : AbstractValidator<ChooseSessionMovieCommand>
    {
        public ChooseSessionMovieCommandValidator()
        {
            RuleFor(c => c.SessionId)
                .NotEmpty().WithMessage("Session ID is required.");

            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("User ID is required.");
        }
    }
}
