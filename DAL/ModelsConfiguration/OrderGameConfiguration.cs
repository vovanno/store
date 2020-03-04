using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelsConfiguration
{
    internal class OrderGameConfiguration : IEntityTypeConfiguration<OrderGame>
    {
        public void Configure(EntityTypeBuilder<OrderGame> builder)
        {
            builder.HasKey(p => new { p.OrderId, p.GameId });
            builder.Property(p => p.GameId).IsRequired();
            builder.Property(p => p.OrderId).IsRequired();
            builder.HasOne(og => og.Order)
                .WithMany(p => p.OrdersGames)
                .HasForeignKey(gg => gg.OrderId);
            builder.HasOne(og => og.Game)
                .WithMany(p => p.OrderGames)
                .HasForeignKey(gg => gg.GameId);
        }
    }
}
