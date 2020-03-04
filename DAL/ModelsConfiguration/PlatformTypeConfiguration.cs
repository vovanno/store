using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelsConfiguration
{
    internal class PlatformTypeConfiguration : IEntityTypeConfiguration<PlatformType>
    {
        public void Configure(EntityTypeBuilder<PlatformType> builder)
        {
            builder.HasKey(p => p.PlatformTypeId);
            builder.Property(p => p.PlatformTypeId).UseMySqlIdentityColumn();
            builder.HasIndex(p => p.Type).IsUnique();
            builder.Property(p => p.Type).HasMaxLength(30).IsRequired();
            builder.HasData(
                new PlatformType{Type = "Windows", PlatformTypeId = 1},
                new PlatformType{Type = "PlayStation", PlatformTypeId = 2},
                new PlatformType {Type = "XBox", PlatformTypeId = 3});
        }
    }
}
