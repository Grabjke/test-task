using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Orders.Commands.AddItem;
using OrderService.Application.Orders.Commands.Create;
using OrderService.Application.Orders.Commands.Remove;
using OrderService.Application.Orders.Commands.RemoveItem;
using OrderService.Application.Orders.Commands.UpdateItem;
using OrderService.Application.Orders.Query.GetItemsWithPagination;
using OrderService.Application.Orders.Query.GetOrdersWithPagination;
using OrderService.Framework;
using OrderService.Presentation.Requests;

namespace OrderService.Presentation.Controllers;

public class OrdersController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateOrderHandler handler,
        [FromBody] CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }

    [HttpPost("{id:guid}/item")]
    public async Task<ActionResult<Guid>> AddItem(
        [FromRoute] Guid id,
        [FromServices] AddItemHandler handler,
        [FromBody] AddItemRequest request,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }

    [HttpGet("items")]
    public async Task<ActionResult> GetItemsWithPagination(
        [FromServices] GetItemsWithPaginationHandler handler,
        [FromQuery] GetItemsWithPaginationRequest request,
        CancellationToken cancellationToken)
    {
        var response = await handler.Handle(request.ToQuery(), cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult> Get(
        [FromServices] GetOrdersWithPaginationHandler handler,
        [FromQuery] GetOrdersWithPaginationRequest request,
        CancellationToken cancellationToken)
    {
        var response = await handler.Handle(request.ToQuery(), cancellationToken);

        return Ok(response);
    }

    [HttpDelete("{id:guid}/hard")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid id,
        [FromServices] RemoveOrderHandler handler,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(new RemoveOrderCommand(id), cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok();
    }

    [HttpDelete("orders/{orderId:guid}/items/{itemId:guid}/hard")]
    public async Task<ActionResult<Guid>> HardDeleteItem(
        [FromRoute] Guid orderId,
        [FromRoute] Guid itemId,
        [FromServices] RemoveItemHandler handler,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(new RemoveItemCommand(orderId, itemId), cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok();
    }
    
    [HttpPut("orders/{orderId:guid}/items/{itemId:guid}")]
    public async Task<ActionResult<Guid>> UpdateItem(
        [FromRoute] Guid orderId,
        [FromRoute] Guid itemId,
        [FromBody] UpdateMainInfoItemRequest request,
        [FromServices] UpdateItemHandler handler,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request.ToCommand(orderId, itemId), cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }
}