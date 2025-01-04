using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.PostTags
{
    public class PostTagConfiguration : IEntityTypeConfiguration<PostTag>
    {
        public void Configure(EntityTypeBuilder<PostTag> builder)
        {
            builder.ToTable("PostTags");

            builder.HasKey(x => new { x.PostId, x.TagId });

            builder.HasOne(x => x.Post)
                   .WithMany(x => x.PostTags)
                   .HasForeignKey(x => x.PostId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Tag)
                   .WithMany(x => x.PostTags)
                   .HasForeignKey(x => x.TagId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
