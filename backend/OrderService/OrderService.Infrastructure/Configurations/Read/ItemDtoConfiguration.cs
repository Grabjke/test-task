using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.Dtos.Query;

namespace OrderService.Infrastructure.Configurations.Read;

public class ItemDtoConfiguration : IEntityTypeConfiguration<ItemDto>
{
    public void Configure(EntityTypeBuilder<ItemDto> builder)
    {
        builder.ToTable("items");

        builder.HasKey(p => p.Id);

        builder.HasOne<OrderDto>()
            .WithMany()
            .HasForeignKey(p => p.OrderId);

        builder.Property(i => i.Description)
            .HasColumnName("description");

        builder.Property(i => i.DiscountType)
            .HasConversion<string>()
            .HasColumnName("discount_type");

        builder.Property(i => i.DiscountValue)
            .HasColumnName("discount_value");

        builder.Property(i => i.Name)
            .HasColumnName("name");

        builder.Property(i => i.PriceAmount)
            .HasColumnName("price_amount");

        builder.Property(i => i.PriceCurrency)
            .HasColumnName("price_currency");

        builder.Property(i => i.Quantity)
            .HasColumnName("quantity");
    }
}