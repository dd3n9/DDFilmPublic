using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.Common.Errors;
using DDFilm.Domain.Common.Exceptions;
using DDFilm.Domain.Common.Models;
using DDFilm.Domain.MovieAggregate.ValueObjects;
using DDFilm.Domain.SessionAggregate.Entities;
using DDFilm.Domain.SessionAggregate.Events;
using DDFilm.Domain.SessionAggregate.ValueObjects;
using FluentResults;

namespace DDFilm.Domain.SessionAggregate
{
    public class Session : AggregateRoot<SessionId>
    {
        public SessionName SessionName { get; private set; }
        public SessionSettings Settings { get; private set; }
        public HashedPassword Password { get; private set; }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;

        private readonly List<SessionParticipant> _participants = new();
        private readonly List<SessionMovie> _sessionMovies = new();
        

        public IReadOnlyList<SessionParticipant> Participants => _participants.AsReadOnly();
        public IReadOnlyList<SessionMovie> SessionMovies => _sessionMovies.AsReadOnly();

        private Session(
            SessionId sessionId,
            SessionName name, 
            SessionSettings setting, 
            HashedPassword password) : base(sessionId)
        {
            SessionName = name;
            Settings = setting;
            Password = password;
        }

        public static Session Create(
            SessionName name, 
            HashedPassword password,
            SessionSettings settings
            )
        {
            var session = new Session(
                SessionId.CreateUnique(),
                name,
                settings,
                password);

            return session;
        }

        private Session() { }

        public Result AddParticipant(ApplicationUserId userId, SessionRole role)
        {
            if (_participants.Any(p => p.UserId == userId))
                return Result.Fail(ApplicationErrors.SessionParticipant.AlreadyExists);

            if (_participants.Count >= Settings.ParticipantLimit)
                return Result.Fail(ApplicationErrors.SessionParticipant.ParticipantLimit);

            var participant = SessionParticipant.Create(userId, role);
            _participants.Add(participant);
            AddEvent(new ParticipantAddedToSession(this, userId));
            return Result.Ok();
        }

        public Result RemoveParticipant(ApplicationUserId userId)
        {
            var participant = _participants.SingleOrDefault(p => p.UserId == userId);

            if (participant is null)      
                return Result.Fail(ApplicationErrors.SessionParticipant.NotFound);

            var result =  _participants.Remove(participant);
            AddEvent(new ParticipantRemovedFromSession(this, userId));

            return Result.Ok();
        }

        public void UpdateOwner(ApplicationUserId newOwnerId)
        {
            var currentOwner = _participants.FirstOrDefault(p => p.Role == SessionRole.Owner);

            if (currentOwner is not null && currentOwner.UserId == newOwnerId)
                throw new UserAlreadyOwnerException();

            var newOwner = _participants.SingleOrDefault(p => p.UserId == newOwnerId);
            if (newOwner is null)
                throw new SessionParticipantNotFoundException(this.SessionName);

            currentOwner?.ChangeRole(SessionRole.User);
            newOwner.ChangeRole(SessionRole.Owner);
            AddEvent(new OwnerInSessionChanged(this.SessionName, newOwnerId));
        }



        public Result AddMovie(SessionMovie sessionMovie)
        {
            var isSessionMovieExists = _sessionMovies.Any(sm => sm.MovieId == sessionMovie.MovieId);
            if (isSessionMovieExists)
                return Result.Fail(ApplicationErrors.SessionMovie.AlreadyExists);

            var numberMoviePerUser = _sessionMovies.Count(sm => sm.IsWatched == false && sm.AddedByUserId == sessionMovie.AddedByUserId);
            if (numberMoviePerUser >= Settings.RequiredMoviesPerUser)
                return Result.Fail(ApplicationErrors.SessionMovie.MoviePerUserLimit);

            _sessionMovies.Add(sessionMovie);
            AddEvent(new SessionMovieAdded(this, sessionMovie));
            return Result.Ok();
        }

        public Result RemoveMovie(MovieId movieId, ApplicationUserId userId)
        {
            var sessionMovie = _sessionMovies.SingleOrDefault(sm => sm.MovieId == movieId);
            if (sessionMovie is null)
                return Result.Fail(ApplicationErrors.SessionMovie.NotFound);

            var isUserMovieOwner = _sessionMovies.Any(sm => sm.AddedByUserId == userId);
            if (!isUserMovieOwner)
                return Result.Fail(ApplicationErrors.SessionMovie.AccessDenial);

            _sessionMovies.Remove(sessionMovie);
            AddEvent(new SessionMovieRemoved(this, sessionMovie.Id));
            return Result.Ok();
        }

        public SessionMovie? GetSessionMovie(MovieId movieId) 
        {
            var movie = _sessionMovies.SingleOrDefault(sm => sm.MovieId == movieId);

            return movie;
        }

        public Result WatchMovie(MovieId movieId)
        {
            var movie = _sessionMovies.SingleOrDefault(sm => sm.MovieId == movieId);
            if (movie is null)
                return Result.Fail(ApplicationErrors.SessionMovie.NotFound);

            return movie.MarkAsWatched();
        }
    }
}
