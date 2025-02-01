using DDFilm.Domain.ApplicationUserAggregate.Entities;
using DDFilm.Domain.ApplicationUserAggregate.Events;
using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.Common.Exceptions;
using DDFilm.Domain.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace DDFilm.Domain.ApplicationUserAggregate
{
    public class ApplicationUser : IdentityUser<ApplicationUserId>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string HashedPassword { get; private set; }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;

        private readonly List<IDomainEvent> _domainEvents = new();
        private readonly List<UserFriend> _friends = new();
        private readonly List<RefreshToken> _refreshTokens = new();

        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        public IReadOnlyList<UserFriend> Friends => _friends.AsReadOnly();
        public IReadOnlyList<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

        private ApplicationUser(
            ApplicationUserId userId,
            string userName,
            string firstName, 
            string lastName,
            string email,
            string hashedPassword) 
        {
            Id = userId;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            HashedPassword = hashedPassword;
        }

        private ApplicationUser() { }

        public static ApplicationUser Create(
            string userName,
            string firstName,
            string lastName, 
            string email,
            string hashedPassword)
        {
            var user = new ApplicationUser(
                ApplicationUserId.CreateUnique(),
                userName, 
                firstName, 
                lastName,
                email,
                hashedPassword
                );

            return user;
        }

        internal void AddFriend(UserFriend friend)
        {
            _friends.Add(friend);
            AddEvent(new UserFriendAdded(friend.UserId, friend.FriendId));
        }

        internal void RemoveFriend(ApplicationUser friend)
        {
            if(!_friends.Any(f => f.FriendId == friend.Id))
            {
                throw new UserFriendNotFoundException(friend);
            }

            _friends.Remove(new UserFriend(this, friend));
            AddEvent(new UserFriendRemoved(this, friend));
        }

        protected void AddEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public RefreshToken AddRefreshToken(RefreshToken refreshToken)
        {
            _refreshTokens.Add(refreshToken);
            return refreshToken;
        }

        public void RemoveRefreshToken(RefreshToken refreshToken)
        {
            _refreshTokens.Remove(refreshToken);
        }

        public void RevokeAllRefreshTokens()
        {
            _refreshTokens.Clear();
        }

        public RefreshToken? FindRefreshToken(Token token)
        {
            return _refreshTokens.FirstOrDefault(rt => rt.Token == token);
        }

        public void ClearDomainEvents() => _domainEvents.Clear();   
    }
}
