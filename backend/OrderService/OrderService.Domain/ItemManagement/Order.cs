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

    private Order(
        OrderId id,
        CustomerId customerId,
        DateTime orderDate,
        Address deliveryAddress) : base(id)
    {
        OrderDate = orderDate;
        DeliveryAddress = deliveryAddress;
        Status = Status.Created;
        CustomerId = customerId;
    }
    public CustomerId CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; }
    public Address DeliveryAddress { get; private set; }
    public Status Status { get; private set; }
    public IReadOnlyList<Item> Items => _items;

    public static Result<Order, Error> Create(
        OrderId id,
        CustomerId customerId,
        DateTime orderDate,
        Address address)
    {
        if (orderDate > DateTime.UtcNow)
            return Errors.General.ValueIsInvalid("OrderDate не может быть в будущем");

        if (orderDate < DateTime.UtcNow.AddYears(-1))
            return Errors.General.ValueIsInvalid("OrderDate слишком старое");

        return new Order(id, customerId, orderDate, address);
    }

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

        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item is null)
            return Errors.General.NotFound();

        _items.Remove(item);
        return Result.Success<Error>();
    }

    public UnitResult<Error> SetStatus(Status newStatus)
    {
        Status = newStatus;
        return Result.Success<Error>();
    }

    public decimal GetOrderTotalAmount() => _items.Sum(item => item.GetTotalWithDiscountNumber());
}