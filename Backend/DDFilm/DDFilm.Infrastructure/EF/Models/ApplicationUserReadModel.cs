
namespace DDFilm.Infrastructure.EF.Models
{
    internal class ApplicationUserReadModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }   
        public string HashedPassword { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<UserFriendReadModel> UserFriends { get; set; }
    }
}
