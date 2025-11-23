using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using OrderService.Core.Abstractions;
using OrderService.Core.Extensions;
using OrderService.Core.ValueObjects.Item;
using OrderService.Domain.ItemManagement;
using OrderService.SharedKernel;
using OrderService.SharedKernel.ValueObjects;

namespace OrderService.Application.Orders.Commands.AddItem;

public class AddItemHandler : ICommandHandler<Guid, AddItemCommand>
{
    private readonly IOrdersRepository _repository;
    private readonly IValidator<AddItemCommand> _validator;
    private readonly ILogger<AddItemHandler> _logger;

    public AddItemHandler(
        IOrdersRepository repository,
        IValidator<AddItemCommand> validator,
        ILogger<AddItemHandler> logger)
    {
        _repository = repository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        AddItemCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        var orderResult = await _repository.GetBy(x => x.Id == command.OrderId, cancellationToken);
        if (orderResult.IsFailure)
            return orderResult.Error.ToErrorList();

        var order = orderResult.Value;
        var itemId = ItemId.NewItemId();
        var itemName = ItemName.Create(command.ItemName).Value;
        var price = Price.Create(command.Price.Amount, command.Price.Currency).Value;
        var quantity = Quantity.Create(command.Quantity).Value;
        var description = Description.Create(command.Description).Value;
        var discount = Discount.Create(command.Discount.Value, (DiscountType)command.Discount.Type).Value;

        var item = new Item(itemId, itemName, price, quantity, description, discount);

        var addResult = order.AddItem(item);
        if (addResult.IsFailure)
            return addResult.Error.ToErrorList();

        await _repository.Save(order, cancellationToken);

        _logger.LogInformation("Item {itemId} has been added", itemId);

        return itemId.Value;
    }
}