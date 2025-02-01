using DDFilm.Domain.ApplicationUserAggregate;
using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.Common.Errors;
using DDFilm.Domain.Common.Exceptions;
using DDFilm.Domain.MovieAggregate;
using DDFilm.Domain.MovieAggregate.Entities;
using DDFilm.Domain.Repositories;
using DDFilm.Domain.SessionAggregate;
using DDFilm.Domain.SessionAggregate.Entities;
using DDFilm.Domain.SessionAggregate.ValueObjects;
using FluentResults;

namespace DDFilm.Domain.Services
{
    public sealed class SessionDomainService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IMovieRepository _movieRepository;

        public SessionDomainService(ISessionRepository sessionRepository,
            IMovieRepository movieRepository)
        {
            _sessionRepository = sessionRepository;
            _movieRepository = movieRepository;
        }

        public async Task<Result> SessionLoginAsync(Session session, ApplicationUser applicationUser, CancellationToken cancellationToken)
        {
            if(session.Participants.Count >= session.Settings.ParticipantLimit)
            {
                return Result.Fail(ApplicationErrors.SessionParticipant.ParticipantLimit);
            }

            var result = session.AddParticipant(applicationUser.Id, SessionRole.User);
            await _sessionRepository.UpdateAsync(session, cancellationToken);
            return result;
        }

        public async Task<Result> SessionLogoutAsync(Session session, ApplicationUser user, CancellationToken cancellationToken)
        {
            if(!IsOwner(session, user.Id)) 
            {
                session.RemoveParticipant(user.Id);
                await _sessionRepository.UpdateAsync(session, cancellationToken);
                return Result.Ok();
            }

            var newOwner = TransferOwnership(session, user.Id);

            if (newOwner is null)
            {
                await _sessionRepository.DeleteAsync(session, cancellationToken);
                return Result.Ok();
            }


            var result = session.RemoveParticipant(user.Id);
            await _sessionRepository.UpdateAsync(session, cancellationToken);
            return result;
        }

        public async Task<Result> DeleteSessionAsync(Session session, ApplicationUser user, CancellationToken cancellationToken) 
        {
            if (!IsOwner(session, user.Id))
                return Result.Fail(ApplicationErrors.Session.AccessDenial);

            await _sessionRepository.DeleteAsync(session, cancellationToken);
            return Result.Ok();
        }

        public async Task<Result<SessionMovie>> ChooseRandomMovieAsync(Session session, ApplicationUser user, CancellationToken cancellationToken)
        {
            if (!session.Participants.Any(p => p.UserId == user.Id))
                return Result.Fail(ApplicationErrors.SessionParticipant.NotFound);

            var lastWatchedMovie = await _movieRepository.GetMovieWithLatestRatingInSessionAsync(session.Id, cancellationToken);

            if(!IsAllUsersRatedSelectedMovie(lastWatchedMovie, session))
                return Result.Fail(ApplicationErrors.SessionMovie.NotAllUsersRatedMovie);

            if (!IsSufficientMoviesAddedByUsers(session))
                return Result.Fail(ApplicationErrors.SessionMovie.NotAllUsersAddedMovies);

            var availableSessionMovies = GetAvailableMovies(session, lastWatchedMovie);

            var random = new Random();
            var randomMovie = availableSessionMovies[random.Next(availableSessionMovies.Count)];

            var movie = await _movieRepository.GetByIdAsync(randomMovie.MovieId, cancellationToken);

            var addMissingRatingsResult = AddMissingRatingsForParticipants(session, movie);
            if (addMissingRatingsResult.IsFailed)
                return addMissingRatingsResult;

            var watchMovieResult = session.WatchMovie(movie.Id);
            if (watchMovieResult.IsFailed)
                return watchMovieResult;

            await _sessionRepository.UpdateAsync(session, cancellationToken);
            await _movieRepository.UpdateAsync(movie, cancellationToken);

            return Result.Ok(randomMovie);
        }

        private bool IsOwner(Session session, ApplicationUserId userId)
        {
            var participant = session.Participants.FirstOrDefault(p => p.UserId == userId);

            if (participant is null)
                throw new SessionParticipantNotFoundException(session.SessionName);

            return participant.Role == SessionRole.Owner;
        }

        private ApplicationUserId? TransferOwnership (Session session, ApplicationUserId currentOwnerId) 
        {
            var nextOwner = session.Participants
                .FirstOrDefault(p => p.UserId != currentOwnerId && p.Role != SessionRole.Owner); 
            
            if(nextOwner is null)
                return null;

            session.UpdateOwner(nextOwner.UserId);
            return nextOwner.UserId;
        }

        private bool IsAllUsersRatedSelectedMovie(Movie? lastWatchedMovie, Session session)
        {
            if (lastWatchedMovie is not null && lastWatchedMovie.Ratings.Any(rm => rm.Rating is null))
                return false;

            return true;
        }

        private bool IsSufficientMoviesAddedByUsers(Session session)
        {
            long totalMoviesRequired = session.Settings.RequiredMoviesPerUser * session.Participants.Count;
            return session.SessionMovies.Count(sm => !sm.IsWatched) >= totalMoviesRequired;
        }

        private List<SessionMovie> GetAvailableMovies(Session session, Movie? lastWatchedMovie)
        {
            var availableSessionMovies = lastWatchedMovie is not null
                ? session.SessionMovies.Where(sm => sm.MovieId != lastWatchedMovie.Id && !sm.IsWatched).ToList()
                : session.SessionMovies.Where(sm => !sm.IsWatched).ToList();

            return availableSessionMovies;
        }

        private Result AddMissingRatingsForParticipants(Session session, Movie movie)
        {
            foreach(var participant in session.Participants)
            {
                if (movie.Ratings.Any(mr => mr.UserId == participant.UserId))
                    continue;

                var movieRating = MovieRating.Create(participant.UserId,
                    session.Id,
                    null
                    );

                var result = movie.AddRating(movieRating);
                if (!result.IsSuccess)
                    return result;
            }

            return Result.Ok();
        }
    }
}
