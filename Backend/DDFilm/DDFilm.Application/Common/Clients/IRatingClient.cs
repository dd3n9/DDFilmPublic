using DDFilm.Contracts.Hubs;

namespace DDFilm.Application.Common.Clients
{
    public interface IRatingClient
    {
        Task ReceiveAllRatings(IEnumerable<RatingProgress> allRatings);
        Task ReceiveNewRating(RatingProgress ratingProgress);
    }
}
