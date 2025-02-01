using FluentValidation;

namespace DDFilm.Application.SessionMovies.Commands.Delete
{
    public sealed class DeleteSessionMovieCommandValidator : AbstractValidator<DeleteSessionMovieCommand>
    {
        public DeleteSessionMovieCommandValidator()
        {
            RuleFor(c => c.SessionId)
                .NotEmpty().WithMessage("Session ID is required.");

            RuleFor(c => c.TmdbId)
                .GreaterThan(0).WithMessage("TMDB ID must be a positive number.");

            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("User ID is required.");
        }
    }
}
