using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.ItemManagement;
using OrderService.SharedKernel;
using OrderService.SharedKernel.ValueObjects;

namespace OrderService.Infrastructure.Configurations.Write;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("items");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .HasConversion(id => id.Value, value => ItemId.Create(value));

        builder.ComplexProperty(i => i.Name, nb =>
        {
            nb.Property(n => n.Value)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        });

        builder.ComplexProperty(i => i.Price, pb =>
        {
            pb.Property(p => p.Amount)
                .IsRequired()
                .HasColumnName("price_amount")
                .HasPrecision(18, 2);

            pb.Property(p => p.Currency)
                .IsRequired()
                .HasColumnName("price_currency")
                .HasMaxLength(8);
        });

        builder.ComplexProperty(i => i.Quantity, qb =>
        {
            qb.Property(q => q.Value)
                .IsRequired()
                .HasColumnName("quantity");
        });

        builder.ComplexProperty(i => i.Description, db =>
        {
            db.Property(d => d.Value)
                .IsRequired()
                .HasColumnName("description")
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
        });

        builder.ComplexProperty(i => i.Discount, disb =>
        {
            disb.Property(d => d.Value)
                .IsRequired()
                .HasColumnName("discount_value")
                .HasPrecision(18, 2);

            disb.Property(d => d.Type)
                .IsRequired()
                .HasColumnName("discount_type");
        });

        builder.Property(i => i.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(i => i.DeletionDate);
    }
}