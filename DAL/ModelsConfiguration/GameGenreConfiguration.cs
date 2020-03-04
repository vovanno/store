using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelsConfiguration
{
    internal class GameGenreConfiguration : IEntityTypeConfiguration<GameGenre>
    {
        public void Configure(EntityTypeBuilder<GameGenre> builder)
        {
            builder.HasKey(p => new { p.GameId, p.GenreId });
            builder.Property(p => p.GameId).IsRequired();
            builder.Property(p => p.GenreId).IsRequired();
            builder.HasOne(gg => gg.Game)
                .WithMany(p => p.GameGenres)
                .HasForeignKey(gg => gg.GameId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(gg => gg.Genre)
                .WithMany(p => p.GameGenres)
                .HasForeignKey(gg => gg.GenreId);
        }
    }
}
