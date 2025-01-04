using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.RefreshTokens
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.UserId);

            builder.Property(x => x.Code)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasOne(x => x.User)
                   .WithOne(x => x.RefreshToken);
        }
    }
}
