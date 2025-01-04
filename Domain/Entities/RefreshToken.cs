namespace Domain.Entities
{
    public class RefreshToken
    {
        public required string UserId { get; set; }
        public User User { get; set; } = default!;
        public required string Code { get; set; }
        public DateTime Expiration { get; set; }
    }
}
