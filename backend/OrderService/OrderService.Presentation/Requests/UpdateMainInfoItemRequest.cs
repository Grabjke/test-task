using OrderService.Application.Orders.Commands.UpdateItem;
using OrderService.Core.Dtos;

namespace OrderService.Presentation.Requests;

public record UpdateMainInfoItemRequest(
    string ItemName,
    PriceDto Price,
    int Quantity,
    string Description,
    DiscountDto Discount)
{
    public UpdateItemCommand ToCommand(Guid orderId, Guid itemId)
        => new(orderId, itemId, ItemName, Price, Quantity, Description, Discount);
}