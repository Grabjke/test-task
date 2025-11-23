namespace OrderService.Core.Dtos.Query;

public class OrderDto
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public DateTime OrderDate { get; init; }
    public string Status { get; init; } = null!;
    public string City { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public string Street { get; init; } = string.Empty;
    public string? ZipCode { get; init; } = string.Empty;
}