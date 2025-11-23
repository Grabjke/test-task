using FluentValidation;
using OrderService.Core.Validation;
using OrderService.SharedKernel;

namespace OrderService.Application.Orders.Commands.RemoveItem;

public class RemoveItemCommandValidator : AbstractValidator<RemoveItemCommand>
{
    public RemoveItemCommandValidator()
    {
        RuleFor(o => o.OrderId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
        
        RuleFor(o => o.ItemId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
    }
}