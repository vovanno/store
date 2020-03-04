using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelsConfiguration
{
    internal class GamePlatformConfiguration : IEntityTypeConfiguration<GamePlatform>
    {
        public void Configure(EntityTypeBuilder<GamePlatform> builder)
        {
            builder.HasKey(p => new { p.GameId, p.PlatformTypeId });
            builder.Property(p => p.GameId).IsRequired();
            builder.Property(p => p.PlatformTypeId).IsRequired();
            builder.HasOne(gp => gp.Game)
                .WithMany(p => p.PlatformTypes)
                .HasForeignKey(gp => gp.GameId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(gp => gp.PlatformType)
                .WithMany(p => p.Games)
                .HasForeignKey(gp => gp.PlatformTypeId);
        }
    }
}
