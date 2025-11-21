namespace OrderService.SharedKernel.ValueObjects;

public record CustomerId
{
    private CustomerId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static CustomerId NewCustomerId() => new(Guid.NewGuid());
    public static CustomerId Empty() => new(Guid.Empty);
    public static CustomerId Create(Guid id) => new(id);
}