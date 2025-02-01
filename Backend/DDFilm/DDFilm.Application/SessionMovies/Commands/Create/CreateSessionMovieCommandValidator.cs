using FluentValidation;

namespace DDFilm.Application.SessionMovies.Commands.Create
{
    public sealed class CreateSessionMovieCommandValidator : AbstractValidator<CreateSessionMovieCommand>
    {
        public CreateSessionMovieCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(c => c.SessionId)
                .NotEmpty().WithMessage("Session ID is required.");

            RuleFor(c => c.TmdbId)
                .GreaterThan(0).WithMessage("TMDB ID must be a positive number.");

            RuleFor(c => c.MovieTitle)
                .NotEmpty().WithMessage("Movie title is required.")
                .MaximumLength(300).WithMessage("Movie title cannot exceed 300 characters.");
        }
    }
}
