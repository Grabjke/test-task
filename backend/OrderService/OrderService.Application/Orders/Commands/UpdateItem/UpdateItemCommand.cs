using OrderService.Core.Abstractions;
using OrderService.Core.Dtos;

namespace OrderService.Application.Orders.Commands.UpdateItem;

public record UpdateItemCommand(
    Guid OrderId,
    Guid ItemId,
    string ItemName,
    PriceDto Price,
    int Quantity,
    string Description,
    DiscountDto Discount) : ICommand;