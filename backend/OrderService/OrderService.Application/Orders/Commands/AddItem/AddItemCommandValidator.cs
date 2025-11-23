using FluentValidation;
using OrderService.Core.Validation;
using OrderService.Core.ValueObjects.Item;
using OrderService.SharedKernel;

namespace OrderService.Application.Orders.Commands.AddItem;

public class AddItemCommandValidator : AbstractValidator<AddItemCommand>
{
    public AddItemCommandValidator()
    {
        RuleFor(o => o.OrderId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
        
        RuleFor(c => c.Description).MustBeValueObject(Description.Create);
        
        RuleFor(c => c.Quantity).MustBeValueObject(Quantity.Create);
        
        RuleFor(c => c.ItemName).MustBeValueObject(ItemName.Create);
        
        RuleFor(c => c.Price)
            .MustBeValueObject(r => Price.Create(r.Amount, r.Currency));
        
        RuleFor(c => c.Discount)
            .MustBeValueObject(r => Discount.Create(r.Value, (DiscountType)r.Type));
    }
}