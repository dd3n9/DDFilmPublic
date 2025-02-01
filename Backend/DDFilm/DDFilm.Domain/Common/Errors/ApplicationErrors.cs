using FluentResults;

namespace DDFilm.Domain.Common.Errors
{
    public class ApplicationErrors
    {
        public static class Session
        {
            public static readonly Error NotFound = new Error("Session was not found.")
                .WithMetadata("ErrorCode", "Session.NotFound");
            public static readonly Error AlreadyExists = new Error("Session already exists.")
                .WithMetadata("ErrorCode", "Session.AlreadyExists");
            public static readonly Error AccessDenial = new Error("Only the owner of the session has the right to delete it.")
                .WithMetadata("ErrorCode", "Session.AccessDenial");
            public static readonly Error IncorrectPassword = new Error("Incorrect session password.")
                .WithMetadata("ErrorCode", "Session.IncorrectPassword");
        }

        public static class SessionParticipant
        {
            public static readonly Error NotFound = new Error("User is not a member of the session.")
                .WithMetadata("ErrorCode", "SessionParticipant.NotFound");
            public static readonly Error AlreadyExists = new Error("User is already a member of the session.")
                .WithMetadata("ErrorCode", "Session.AlreadyExists");
            public static readonly Error ParticipantLimit = new Error("Maximum number of participants in session has been reached.")
                .WithMetadata("ErrorCode", "SessionParticipant.ParticipantLimit");
        }

        public static class SessionMovie
        {
            public static readonly Error NotFound = new Error("Movie not found in the session.")
                .WithMetadata("ErrorCode", "Session.NotFound");
            public static readonly Error AlreadyExists = new Error("Movie has been already added to the session.")
                .WithMetadata("ErrorCode", "SessionMovie.AlreadyExists");
            public static readonly Error MoviePerUserLimit = new Error("The maximum number of movies for a participant has been added")
                .WithMetadata("ErrorCode", "SessionMovie.MoviePerUserLimit");
            public static readonly Error AccessDenial = new Error("Only the owner of the movie has the right to delete it.")
                .WithMetadata("ErrorCode", "SessionMovie.AccessDenial");
            public static readonly Error NotAllUsersAddedMovies = new Error("Not all users have added movies to watch.")
                .WithMetadata("ErrorCode", "SessionMovie.NotAllUsersAddedMovies");
            public static readonly Error NotAllUsersRatedMovie = new Error("Not all users have rated the movie yet.")
                .WithMetadata("ErrorCode", "SessionMovie.NotAllUsersRatedMovie");
            public static readonly Error AlreadyWatched = new Error("The movie has already been marked as watched.")
                .WithMetadata("ErrorCode", "SessionMovie.AlreadyWatched");
        }

        public static class ApplicationUser
        {
            public static readonly Error NotFound = new Error("User was not found.")
                .WithMetadata("ErrorCode", "ApplicationUser.NotFound");
            public static readonly Error AlreadyExistsByEmail = new Error("Email is already taken.")
                .WithMetadata("ErrorCode", "ApplicationUser.AlreadyExistsByEmail");
            public static Error CustomValidationError(string message) => new Error(message)
                .WithMetadata("ErrorCode", "ApplicationUser.Validation");
        }

        public static class Authentication
        {
            public static readonly Error IncorrectPassword = new Error("Incorrect password.")
                .WithMetadata("ErrorCode", "Authentication.IncorrectPassword");
            public static readonly Error InvalidCredentials = new Error("Invalid email or password.")
                .WithMetadata("ErrorCode", "Authentication.InvalidCredentials");
            public static readonly Error InvalidToken = new Error("Invalid authentication token.")
                .WithMetadata("ErrorCode", "Authentication.InvalidToken");
            public static readonly Error ExpiredToken = new Error("Authentication token has expired.")
                 .WithMetadata("ErrorCode", "Authentication.ExpiredToken");
            public static readonly Error RefreshTokenNotFound = new Error("Refresh token not found.")
              .WithMetadata("ErrorCode", "Authentication.RefreshTokenNotFound");
            public static readonly Error RefreshTokenExpired = new Error("Refresh token has expired.")
             .WithMetadata("ErrorCode", "Authentication.RefreshTokenExpired");
        }

        public static class Movie
        {
            public static readonly Error NotFound = new Error("Movie was not found")
                .WithMetadata("ErrorCode", "Movie.NotFound");
        }

        public static class MovieRating
        {
            public static readonly Error AlreadyRated = new Error("This movie has already been rated by the user.")
                .WithMetadata("ErrorCode", "MovieRating.MovieAlreadyRated");
            public static readonly Error NotFound = new Error("Movie ratig was not found")
                .WithMetadata("ErrorCode", "MovieRating.NotFound");
        }
    }
}
