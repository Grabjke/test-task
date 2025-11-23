using OrderService.Core.Abstractions;

namespace OrderService.Application.Orders.Commands.RemoveItem;

public record RemoveItemCommand(Guid OrderId, Guid ItemId) : ICommand;