using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using OrderService.Core.Abstractions;
using OrderService.Core.Extensions;
using OrderService.SharedKernel;
using OrderService.SharedKernel.ValueObjects;

namespace OrderService.Application.Orders.Commands.RemoveItem;

public class RemoveItemHandler : ICommandHandler<RemoveItemCommand>
{
    private readonly IOrdersRepository _repository;
    private readonly IValidator<RemoveItemCommand> _validator;
    private readonly ILogger<RemoveItemHandler> _logger;

    public RemoveItemHandler(
        IOrdersRepository repository,
        IValidator<RemoveItemCommand> validator,
        ILogger<RemoveItemHandler> logger)
    {
        _repository = repository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        RemoveItemCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        var orderResult = await _repository.GetBy(o => o.Id == command.OrderId, cancellationToken);
        if (orderResult.IsFailure)
            return orderResult.Error.ToErrorList();

        var order = orderResult.Value;
        var itemId = ItemId.Create(command.ItemId);

        var removeResult = order.RemoveItem(itemId);
        if (removeResult.IsFailure)
            return removeResult.Error.ToErrorList();

        await _repository.Save(order, cancellationToken);

        _logger.LogInformation("Item {itemId} has been removed", itemId);

        return Result.Success<ErrorList>();
    }
}