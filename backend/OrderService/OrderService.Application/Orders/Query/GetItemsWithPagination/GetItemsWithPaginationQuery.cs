using OrderService.Core.Abstractions;

namespace OrderService.Application.Orders.Query.GetItemsWithPagination;

public record GetItemsWithPaginationQuery(
    Guid? OrderId,
    string? Name,
    decimal? Price,
    int? Quantity,
    string? Description,
    decimal? Discount,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize) : IQuery;