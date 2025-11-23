using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using OrderService.Core.Abstractions;
using OrderService.Core.Extensions;
using OrderService.SharedKernel;

namespace OrderService.Application.Orders.Commands.Remove;

public class RemoveOrderHandler : ICommandHandler<RemoveOrderCommand>
{
    private readonly IOrdersRepository _repository;
    private readonly ILogger<RemoveOrderHandler> _logger;
    private readonly IValidator<RemoveOrderCommand> _validator;

    public RemoveOrderHandler(
        IOrdersRepository repository,
        ILogger<RemoveOrderHandler> logger,
        IValidator<RemoveOrderCommand> validator)
    {
        _repository = repository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        RemoveOrderCommand command,
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

        await _repository.Delete(orderResult.Value, cancellationToken);

        return Result.Success<ErrorList>();
    }
}