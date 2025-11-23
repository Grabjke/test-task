namespace OrderService.Core.Dtos.Query;

public class ItemDto
{
    public Guid Id { get; init; }
    public Guid OrderId { get; init; }
    public string Description { get; init; } = string.Empty;
    public string DiscountType { get; init; } = string.Empty;
    public decimal DiscountValue { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal PriceAmount { get; init; }
    public string PriceCurrency { get; init; } = string.Empty;
    public int Quantity { get; init; }
}