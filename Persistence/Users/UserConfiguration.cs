using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(x => x.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.HasMany(x => x.Posts)
                   .WithOne(x => x.Author)
                   .HasForeignKey(x => x.AuthorId);
        }
    }
}
