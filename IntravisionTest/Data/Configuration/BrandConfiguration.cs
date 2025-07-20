using IntravisionTest.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntravisionTest.Data.Configuration
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(b => b.BrandId);

            builder.HasMany(b => b.Drinks)
                .WithOne(d => d.Brand);

            builder.Property(d => d.BrandName)
                .IsRequired();
        }
    }
}
