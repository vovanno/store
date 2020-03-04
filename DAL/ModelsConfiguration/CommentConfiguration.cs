using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelsConfiguration
{
    internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(p => p.CommentId);
            builder.Property(p => p.CommentId).UseMySqlIdentityColumn();
            builder.Property(p => p.Name).HasMaxLength(30).IsRequired();
            builder.Property(p => p.Body).HasMaxLength(300).IsRequired();
            builder.Property(p => p.GameId).IsRequired();
            builder.Property(p => p.ParentCommentId).IsRequired(false);
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
