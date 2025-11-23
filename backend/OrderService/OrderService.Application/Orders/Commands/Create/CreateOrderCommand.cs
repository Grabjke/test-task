using OrderService.Core.Abstractions;
using OrderService.Core.Dtos;

namespace OrderService.Application.Orders.Commands.Create;

public record CreateOrderCommand(Guid CustomerId, AddressDto Address) : ICommand;