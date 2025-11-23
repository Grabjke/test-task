using CSharpFunctionalExtensions;
using OrderService.Core.ValueObjects.Item;
using OrderService.Core.ValueObjects.Order;
using OrderService.SharedKernel;
using OrderService.SharedKernel.ValueObjects;

namespace OrderService.Domain.ItemManagement;

public class Order : SoftDeletableEntity<OrderId>
{
    private readonly List<Item> _items = [];

    // EF Core 
    private Order(OrderId id) : base(id)
    {
    }

    public Order(
        OrderId id,
        CustomerId customerId,
        Address deliveryAddress) : base(id)
    {
        OrderDate = DateTime.UtcNow;
        DeliveryAddress = deliveryAddress;
        Status = Status.Created;
        CustomerId = customerId;
    }
    public CustomerId CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; }
    public Address DeliveryAddress { get; private set; }
    public Status Status { get; private set; }
    public IReadOnlyList<Item> Items => _items;

    public UnitResult<Error> AddItem(Item item)
    {
        if (_items.Any(i => i.Id == item.Id))
            return Errors.General.AllReadyExist();

        _items.Add(item);
        return Result.Success<Error>();
    }

    public UnitResult<Error> RemoveItem(ItemId itemId)
    {
        if (Status != Status.Created && Status != Status.Pending)
            return Errors.General.Failure();

        var item = _items.FirstOrDefault(i => i.Id.Value == itemId.Value);
        if (item is null)
            return Errors.General.NotFound();

        _items.Remove(item);
        return Result.Success<Error>();
    }

    public UnitResult<Error> UpdateMainInfoItem(
        ItemId itemId,
        ItemName name,
        Price price,
        Quantity quantity,
        Description description,
        Discount discount)
    {
        var item = _items.FirstOrDefault(i => i.Id.Value == itemId.Value);
        if (item is null)
            return Errors.General.NotFound();

        item.UpdateMainInfo(name, price, quantity, description, discount);

        return Result.Success<Error>();
    }

    public UnitResult<Error> SetStatus(Status newStatus)
    {
        Status = newStatus;

        return Result.Success<Error>();
    }

    public decimal GetOrderTotalAmount() => _items.Sum(item => item.GetTotalWithDiscountNumber());
}