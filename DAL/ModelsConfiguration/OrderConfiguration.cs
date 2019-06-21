using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelsConfiguration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(p => p.OrderId);
            builder.Property(p => p.OrderDate).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.ShippedDate);
        }
    }
}
