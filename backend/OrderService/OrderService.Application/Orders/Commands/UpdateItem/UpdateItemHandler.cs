using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using OrderService.Core.Abstractions;
using OrderService.Core.Extensions;
using OrderService.Core.ValueObjects.Item;
using OrderService.SharedKernel;
using OrderService.SharedKernel.ValueObjects;

namespace OrderService.Application.Orders.Commands.UpdateItem;

public class UpdateItemHandler : ICommandHandler<Guid, UpdateItemCommand>
{
    private readonly IOrdersRepository _repository;
    private readonly ILogger<UpdateItemHandler> _logger;
    private readonly IValidator<UpdateItemCommand> _validator;

    public UpdateItemHandler(
        IOrdersRepository repository,
        ILogger<UpdateItemHandler> logger,
        IValidator<UpdateItemCommand> validator)
    {
        _repository = repository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateItemCommand command,
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

        var itemId = ItemId.Create(command.ItemId);
        var itemName = ItemName.Create(command.ItemName).Value;
        var price = Price.Create(command.Price.Amount, command.Price.Currency).Value;
        var quantity = Quantity.Create(command.Quantity).Value;
        var description = Description.Create(command.Description).Value;
        var discount = Discount.Create(command.Discount.Value,(DiscountType)command.Discount.Type).Value;

        var updateResult = orderResult.Value.UpdateMainInfoItem(
            itemId,
            itemName,
            price,
            quantity,
            description,
            discount);
        if (updateResult.IsFailure)
            return updateResult.Error.ToErrorList();

        await _repository.Save(orderResult.Value, cancellationToken);

        _logger.LogInformation("Update item with id:{itemId} to order with id: {orderId}",
            command.OrderId, command.ItemId);

        return command.ItemId;
    }
}