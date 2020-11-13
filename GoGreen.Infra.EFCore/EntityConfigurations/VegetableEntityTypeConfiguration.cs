using GoGreen.Domain.Aggregates.VegetableAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoGreen.Infra.EFCore.EntityConfigurations
{
    class VegetableEntityTypeConfiguration : IEntityTypeConfiguration<Vegetable>
    {
        public void Configure(EntityTypeBuilder<Vegetable> builder)
        {
            builder.ToTable("vegetables");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedOnAdd();
            builder.Property(o => o.RowVersion).IsRowVersion().IsConcurrencyToken();

            builder.Property(o => o.Name).IsRequired();
            builder.Property(o => o.Price).IsRequired();
        }
    }
}
