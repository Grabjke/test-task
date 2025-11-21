using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Core.Extensions;
using OrderService.Domain.ItemManagement;
using OrderService.SharedKernel;
using OrderService.SharedKernel.ValueObjects;

namespace OrderService.Infrastructure.Configurations.Write;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .HasConversion(id => id.Value, value => OrderId.Create(value));

        builder.Property(o => o.CustomerId)
            .HasConversion(id => id.Value, value => CustomerId.Create(value))
            .HasColumnName("customer_id")
            .IsRequired();

        builder.Property(o => o.OrderDate)
            .HasColumnName("order_date")
            .SetDefaultDateTimeKind(DateTimeKind.Utc)
            .IsRequired();

        builder.ComplexProperty(o => o.DeliveryAddress, address =>
        {
            address.Property(a => a.Street)
                .IsRequired()
                .HasColumnName("street")
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
            address.Property(a => a.City)
                .IsRequired()
                .HasColumnName("city")
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
            address.Property(a => a.Country)
                .IsRequired()
                .HasColumnName("country")
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
            address.Property(a => a.ZipCode)
                .HasColumnName("zip_code")
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        });

        builder.Property(o => o.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(o => o.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(o => o.DeletionDate);

        builder.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey("order_id")
            .IsRequired();
    }
}