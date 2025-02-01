
namespace DDFilm.Infrastructure.EF.Models
{
    internal class UserFriendReadModel : BaseReadModel
    {
        public string UserId { get; set; }
        public string FriendId { get; set; }
    }
}
