using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DAL.Entities;

namespace DAL.ModelsConfiguration
{
    internal class PlatformTypeConfiguration : IEntityTypeConfiguration<PlatformType>
    {
        public void Configure(EntityTypeBuilder<PlatformType> builder)
        {
            builder.HasKey(p => p.PlatformTypeId);
            builder.HasIndex(p => p.Type).IsUnique();
            builder.Property(p => p.Type).HasMaxLength(30).IsRequired();
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
