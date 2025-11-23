using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.Dtos.Query;

namespace OrderService.Infrastructure.Configurations.Read;

public class OrderDtoConfiguration : IEntityTypeConfiguration<OrderDto>
{
    public void Configure(EntityTypeBuilder<OrderDto> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.CustomerId)
            .HasColumnName("customer_id");

        builder.Property(o => o.OrderDate)
            .HasColumnName("order_date");

        builder.Property(o => o.Status)
            .HasConversion<string>()
            .HasColumnName("status");

        builder.Property(o => o.City)
            .HasColumnName("city");

        builder.Property(o => o.Country)
            .HasColumnName("country");

        builder.Property(o => o.Street)
            .HasColumnName("street");

        builder.Property(o => o.ZipCode)
            .HasColumnName("zip_code");
    }
}