using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DAL.Entities;

namespace DAL.ModelsConfiguration
{
    internal class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(p => p.GenreId);
            builder.HasIndex(p => p.Name).IsUnique();
            builder.Property(p => p.Name).HasMaxLength(30).IsRequired();
            builder.HasQueryFilter(p => !p.IsDeleted);
            builder.HasData(
                new Genre() { GenreId = 1, Name = "Strategy" },
                new Genre() { GenreId = 2, Name = "RPG" },
                new Genre() { GenreId = 3, Name = "Sports" },
                new Genre() { GenreId = 4, Name = "Races" },
                new Genre() { GenreId = 5, Name = "Action" },
                new Genre() { GenreId = 6, Name = "Adventure" },
                new Genre() { GenreId = 7, Name = "Puzzle & Skills" },
                new Genre() { GenreId = 8, Name = "Misc" },
                new Genre() { GenreId = 9, Name = "Other" }
                );
        }
    }

    internal class SubGenreConfiguration : IEntityTypeConfiguration<Genre.SubGenre>
    {
        public void Configure(EntityTypeBuilder<Genre.SubGenre> builder)
        {
            builder.HasKey(p => p.SubGenreId);
            builder.HasIndex(p => p.Name).IsUnique();
            builder.Property(p => p.Name).HasMaxLength(30).IsRequired();
            builder.Property(p => p.GenreId).IsRequired();
            builder.HasQueryFilter(p => !p.IsDeleted);
            builder.HasData(
                new Genre.SubGenre() { SubGenreId = 1, Name = "RTC", GenreId = 1 },
                new Genre.SubGenre() { SubGenreId = 2, Name = "TBS", GenreId = 1 },
                new Genre.SubGenre() { SubGenreId = 3, Name = "Rally", GenreId = 4 },
                new Genre.SubGenre() { SubGenreId = 4, Name = "Arcade", GenreId = 4 },
                new Genre.SubGenre() { SubGenreId = 5, Name = "Formula", GenreId = 4 },
                new Genre.SubGenre() { SubGenreId = 6, Name = "Off-Road", GenreId = 4 },
                new Genre.SubGenre() { SubGenreId = 7, Name = "FPS", GenreId = 5 },
                new Genre.SubGenre() { SubGenreId = 8, Name = "TPS", GenreId = 5 },
                new Genre.SubGenre() { SubGenreId = 9, Name = "Misc", GenreId = 5 }
                );
        }
    }
}
