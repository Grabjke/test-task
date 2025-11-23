using FluentValidation;
using OrderService.Core.Validation;
using OrderService.Core.ValueObjects.Order;
using OrderService.SharedKernel;

namespace OrderService.Application.Orders.Commands.Create;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(o => o.CustomerId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
        
        RuleFor(x => x.Address)
            .MustBeValueObject(c => Address.Create(c.Street, c.City, c.Country, c.ZipCode));
    }
}