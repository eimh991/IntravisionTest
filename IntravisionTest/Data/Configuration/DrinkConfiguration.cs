using IntravisionTest.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntravisionTest.Data.Configuration
{
    public class DrinkConfiguration : IEntityTypeConfiguration<Drink>
    {
        public void Configure(EntityTypeBuilder<Drink> builder)
        {
            builder.HasKey(d => d.DrinkId);

            builder.HasOne(d => d.Brand)
                .WithMany(b => b.Drinks);

            builder.Property(d=>d.Stock)
                .IsRequired();

            builder.Property(d => d.DrinkName)
                .IsRequired();

            builder.Property(d => d.Price)
                .IsRequired();
        }
    }
}
