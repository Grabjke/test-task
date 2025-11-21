using CSharpFunctionalExtensions;
using OrderService.Core.ValueObjects.Item;
using OrderService.SharedKernel;
using OrderService.SharedKernel.ValueObjects;

namespace OrderService.Domain.ItemManagement;

public class Item : SoftDeletableEntity<ItemId>
{
    // EF Core 
    private Item(ItemId id) : base(id)
    {
    }

    public Item(
        ItemId id,
        ItemName itemName,
        Money price,
        Quantity quantity,
        Description description,
        Discount discount
    ) : base(id)
    {
        Name = itemName;
        Price = price;
        Quantity = quantity;
        Description = description;
        Discount = discount;
    }

    public ItemName Name { get; private set; }
    public Money Price { get; private set; }
    public Quantity Quantity { get; private set; }
    public Description Description { get; private set; }
    public Discount Discount { get; private set; }

    internal UnitResult<Error> UpdateMainInfo(ItemName name, Money price, Quantity quantity, Description description)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
        Description = description;

        return Result.Success<Error>();
    }
    
    internal UnitResult<Error> ChangePrice(Money newPrice)
    {
        Price = newPrice;
        
        return Result.Success<Error>();
    }
    
    internal UnitResult<Error> ChangeQuantity(Quantity newQuantity)
    {
        Quantity = newQuantity;
        
        return Result.Success<Error>();
    }
    
    internal UnitResult<Error> RemoveDiscount()
    {
        Discount = Discount.Create(0, DiscountType.Fixed).Value;
        
        return Result.Success<Error>();
    }
    
    internal UnitResult<Error> UpdateDescription(Description description)
    {
        Description = description;
        return Result.Success<Error>();
    }
    
    internal decimal GetTotalWithDiscountNumber()
    {
        decimal totalForOne;
        if (Discount.Type == DiscountType.Percent)
        {
            totalForOne = Price.Amount * (1 - Discount.Value / 100m);
        }
        else 
        {
            totalForOne = Price.Amount - Discount.Value;
        }
        totalForOne = Math.Max(0, totalForOne);

        return totalForOne * Quantity.Value;
    }
}