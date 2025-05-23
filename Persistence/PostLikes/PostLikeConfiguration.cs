﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.PostLikes
{
    public class PostLikeConfiguration : IEntityTypeConfiguration<PostLike>
    {
        public void Configure(EntityTypeBuilder<PostLike> builder)
        {
            builder.HasKey(x => new { x.UserId, x.PostId });

            builder.HasOne(x => x.User)
                   .WithMany(x => x.PostLikes)
                   .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Post)
                   .WithMany(x => x.PostLikes)
                   .HasForeignKey(x => x.PostId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
