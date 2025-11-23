using FluentValidation;
using OrderService.Core.Validation;
using OrderService.SharedKernel;

namespace OrderService.Application.Orders.Commands.Remove;

public class RemoveOrderCommandValidator : AbstractValidator<RemoveOrderCommand>
{
    public RemoveOrderCommandValidator()
    {
        RuleFor(o => o.OrderId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
    }
}