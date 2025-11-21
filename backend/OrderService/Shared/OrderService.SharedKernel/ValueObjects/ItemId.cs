namespace OrderService.SharedKernel.ValueObjects;

public class ItemId : IComparable<ItemId>
{
    private ItemId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ItemId NewItemId() => new(Guid.NewGuid());
    public static ItemId Empty() => new(Guid.Empty);
    public static ItemId Create(Guid id) => new(id);


    public int CompareTo(ItemId? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        return other is null ? 1 : Value.CompareTo(other.Value);
    }
}