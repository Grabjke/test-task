using OrderService.Application.Orders.Commands.AddItem;
using OrderService.Core.Dtos;

namespace OrderService.Presentation.Requests;

public record AddItemRequest(
    string ItemName,
    PriceDto Price,
    int Quantity,
    string Description,
    DiscountDto Discount)
{
    public AddItemCommand ToCommand(Guid orderId)
        => new(orderId, ItemName, Price, Quantity, Description, Discount);
}