using FluentValidation;

namespace DDFilm.Application.Movies.Commands.RateMovie
{
    public sealed class RateMovieCommandValidator : AbstractValidator<RateMovieCommand>
    {
        public RateMovieCommandValidator()
        {
            RuleFor(command => command.TmdbId)
                .GreaterThan(0).WithMessage("The TMDB ID must be a positive number.");

            RuleFor(command => command.UserId)
                .NotEmpty().WithMessage("User ID is required.")
                .Must(BeAValidGuid).WithMessage("Invalid User ID.");

            RuleFor(command => command.Rating)
                .InclusiveBetween(1, 10).WithMessage("Rating must be between 1 and 10.");
        }

        private bool BeAValidGuid(string userId)
        {
            return Guid.TryParse(userId, out _);
        }
    }
}
