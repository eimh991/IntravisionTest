using IntravisionTest.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntravisionTest.Data.Configuration
{
    public class CoinConfiguration : IEntityTypeConfiguration<Coin>
    {
        public void Configure(EntityTypeBuilder<Coin> builder)
        {
            builder.HasKey(c=>c.CoinId);

            builder.Property(c=>c.Quantity).IsRequired();
            builder.Property(c=>c.Value).IsRequired();
        }
    }
}
