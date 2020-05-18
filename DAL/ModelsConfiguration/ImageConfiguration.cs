using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelsConfiguration
{
    internal class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(p => p.ImageId);
            builder.Property(p => p.ImageId).UseMySqlIdentityColumn();
            builder.Property(p => p.ImageKey).IsRequired();
            builder.Property(p => p.IsMain).IsRequired().HasDefaultValue(false);
        }
    }
}
