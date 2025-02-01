using FluentValidation;

namespace DDFilm.Application.SessionMovies.Queries.GetWatchingSessionMovie
{
    public sealed class GetWatchingSessionMovieQueryValidator : AbstractValidator<GetWatchingSessionMovieQuery>
    {
        public GetWatchingSessionMovieQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(q => q.SessionId)
                .NotEmpty().WithMessage("Session ID is required.");
        }
    }
}
