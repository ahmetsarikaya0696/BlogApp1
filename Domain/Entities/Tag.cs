namespace Domain.Entities
{
    public class Tag : BaseEntity
    {
        public required string Name { get; set; }
        public List<PostTag>? PostTags { get; set; }
    }
}
