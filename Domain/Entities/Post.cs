using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Post : BaseEntity, IAuditEntity
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public int ViewCount { get; set; }

        public required string AuthorId { get; set; }

        [JsonIgnore]
        public User Author { get; set; } = default!;

        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        [JsonIgnore]
        public List<PostTag>? PostTags { get; set; }

        [JsonIgnore]
        public List<PostLike>? PostLikes { get; set; }
    }
}
