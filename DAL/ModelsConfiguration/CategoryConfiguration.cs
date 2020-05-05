using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelsConfiguration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(p => p.CategoryId);
            builder.Property(p => p.CategoryId).UseMySqlIdentityColumn();
            builder.HasIndex(p => p.Name).IsUnique();
            builder.Property(p => p.Name).HasMaxLength(30).IsRequired();
        }
    }
}
