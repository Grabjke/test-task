using OrderService.Core.Abstractions;
using OrderService.Core.Dtos;

namespace OrderService.Application.Orders.Commands.AddItem;

public record AddItemCommand(
    Guid OrderId,
    string ItemName,
    PriceDto Price,
    int Quantity,
    string Description,
    DiscountDto Discount) : ICommand;