namespace Domain.Entities
{
    public class PostLike
    {
        public required string UserId { get; set; }
        public User User { get; set; } = default!;

        public required Guid PostId { get; set; }
        public Post Post { get; set; } = default!;
    }
}
