using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public List<Post>? Posts { get; set; }
        public List<PostLike>? PostLikes { get; set; }
        public RefreshToken? RefreshToken { get; set; }
    }
}
