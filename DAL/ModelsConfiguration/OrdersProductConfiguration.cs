using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DAL.ModelsConfiguration
{
    public class OrdersProductConfiguration : IEntityTypeConfiguration<OrdersProduct>
    {
        public void Configure(EntityTypeBuilder<OrdersProduct> builder)
        {
            builder.HasKey(p => new { p.OrderId, p.ProductId });
            builder.Property(p => p.OrderId).IsRequired();
            builder.Property(p => p.ProductId).IsRequired();
            builder.HasOne(p => p.Order)
                .WithMany(o => o.Products)
                .HasForeignKey(p => p.OrderId);
            builder.HasOne(p => p.Product)
                .WithMany(p => p.Orders)
                .HasForeignKey(p => p.ProductId);
        }
    }
}
