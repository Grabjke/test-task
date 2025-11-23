using OrderService.Core.Abstractions;

namespace OrderService.Application.Orders.Query.GetOrdersWithPagination;

public record GetOrdersWithPaginationQuery(int Page, int PageSize) : IQuery;