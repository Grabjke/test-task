using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using OrderService.Core.Abstractions;
using OrderService.Core.Extensions;
using OrderService.Core.ValueObjects.Order;
using OrderService.Domain.ItemManagement;
using OrderService.SharedKernel;
using OrderService.SharedKernel.ValueObjects;

namespace OrderService.Application.Orders.Commands.Create;

public class CreateOrderHandler : ICommandHandler<Guid, CreateOrderCommand>
{
    private readonly IOrdersRepository _repository;
    private readonly IValidator<CreateOrderCommand> _validator;
    private readonly ILogger<CreateOrderHandler> _logger;

    public CreateOrderHandler(
        IOrdersRepository repository,
        IValidator<CreateOrderCommand> validator,
        ILogger<CreateOrderHandler> logger)
    {
        _repository = repository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        CreateOrderCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        var orderId = OrderId.NewOrderId();
        var customerId = CustomerId.Create(command.CustomerId);
        var address = Address.Create(
            command.Address.Street,
            command.Address.City,
            command.Address.Country,
            command.Address.ZipCode).Value;

        var order = new Order(orderId, customerId, address);

        await _repository.Add(order, cancellationToken);
        
        _logger.LogInformation("Created order with id {Id}", orderId);

        return orderId.Value;
    }
}