using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelsConfiguration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);
            builder.Property(p => p.ProductId).UseMySqlIdentityColumn();
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(500).IsRequired(false);
            builder.Property(p => p.CategoryId).IsRequired();
            builder.Property(p => p.ManufacturerId).IsRequired();
            builder.Property(p => p.ManufacturerId).IsRequired();
            builder.Property(p => p.Availability).IsRequired().HasDefaultValue(false);
            builder.Ignore(p => p.Rating);
        }
    }
}
