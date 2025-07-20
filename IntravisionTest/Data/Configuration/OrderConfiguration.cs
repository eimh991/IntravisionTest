using IntravisionTest.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntravisionTest.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.OrderId);

            builder.HasMany(o=>o.OrderItems)
                .WithOne(oi=>oi.Order);

            builder.Property(o => o.TotalPrice)
                .IsRequired();
        }
    }
}
