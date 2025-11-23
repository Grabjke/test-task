namespace OrderService.SharedKernel.ValueObjects;

public record OrderId : IComparable<OrderId>
{
    private OrderId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static OrderId NewOrderId() => new(Guid.NewGuid());
    public static OrderId Empty() => new(Guid.Empty);
    public static OrderId Create(Guid id) => new(id);
    
    public static implicit operator Guid(OrderId orderId)
    {
        ArgumentNullException.ThrowIfNull(orderId);
        return orderId.Value;
    }
    public int CompareTo(OrderId? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        return other is null ? 1 : Value.CompareTo(other.Value);
    }
}