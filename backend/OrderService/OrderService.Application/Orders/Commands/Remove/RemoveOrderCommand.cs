using OrderService.Core.Abstractions;

namespace OrderService.Application.Orders.Commands.Remove;

public record RemoveOrderCommand(Guid OrderId) : ICommand;