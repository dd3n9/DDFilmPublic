using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.Common.Models;

namespace DDFilm.Domain.ApplicationUserAggregate.Entities
{
    public sealed class RefreshToken : Entity<RefreshTokenId>
    {
        public Token Token { get; private set; }
        public JwtId JwtId { get; private set; }
        public DateTime AddedDate { get; private set; }
        public DateTime ExpiryDate { get; private set; }

        private RefreshToken() { }

        private RefreshToken(RefreshTokenId refreshTokenId, 
            Token token,
            JwtId jwtId, 
            DateTime addedDate, 
            DateTime expiryDate) : base(refreshTokenId) 
        {
            Token = token;
            JwtId = jwtId;
            AddedDate = addedDate;
            ExpiryDate = expiryDate;
        }

        public static RefreshToken Create(JwtId jwtId,
            DateTime addedDate,
            DateTime expiryDate)
        {
            var refreshToken = new RefreshToken(RefreshTokenId.CreateUnique(),
                Token.CreateUnique(), jwtId, addedDate, expiryDate);

            return refreshToken;
        }
    }
}
