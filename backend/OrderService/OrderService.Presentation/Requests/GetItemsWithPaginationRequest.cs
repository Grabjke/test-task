using OrderService.Application.Orders.Query.GetItemsWithPagination;

namespace OrderService.Presentation.Requests;

public record GetItemsWithPaginationRequest(
    Guid? OrderId,
    string? Name,
    decimal? Price,
    int? Quantity,
    string? Description,
    decimal? Discount,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize)
{
    public GetItemsWithPaginationQuery ToQuery()
        => new(OrderId, Name, Price, Quantity, Description, Discount, SortBy, SortDirection, Page, PageSize);
}