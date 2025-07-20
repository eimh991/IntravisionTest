using IntravisionTest.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntravisionTest.Data.Configuration
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(ci => ci.CartItemId);

            builder.Property(ci => ci.Quantity).IsRequired();

            builder.HasOne(ci => ci.Drink)
                .WithMany().HasForeignKey(ci => ci.DrinkId);
        }
    }
}
