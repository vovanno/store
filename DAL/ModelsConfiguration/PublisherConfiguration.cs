using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DAL.Entities;

namespace DAL.ModelsConfiguration
{
    internal class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasKey(p => p.PublisherId);
            builder.HasIndex(p => p.Name).IsUnique();
            builder.Property(p => p.Name).HasMaxLength(30).IsRequired();
            builder.HasQueryFilter(p => !p.IsDeleted);
            builder.HasData(new Publisher() { Name = "unknown", IsDeleted = false, PublisherId = 2 });
        }
    }
}
