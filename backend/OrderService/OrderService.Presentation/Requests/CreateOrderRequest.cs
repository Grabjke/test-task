using OrderService.Application.Orders.Commands.Create;
using OrderService.Core.Dtos;

namespace OrderService.Presentation.Requests;

public record CreateOrderRequest(
    Guid CustomerId,
    AddressDto Address)
{
    public CreateOrderCommand ToCommand()
        => new(CustomerId, Address);
}