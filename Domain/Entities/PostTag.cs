namespace Domain.Entities
{
    public class PostTag
    {
        public required Guid PostId { get; set; }
        public Post Post { get; set; } = default!;

        public required Guid TagId { get; set; }
        public Tag Tag { get; set; }  = default!;
    }
}
