using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Posts
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedNever();

            builder.Property(x => x.Title)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.Property(x => x.Content)
                   .IsRequired()
                   .HasColumnType("nvarchar(max)");

            builder.Property(x => x.CreatedDate)
                   .IsRequired();

            builder.HasIndex(x => x.AuthorId);
        }
    }
}
