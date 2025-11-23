using OrderService.Core;
using OrderService.Core.Abstractions;
using OrderService.Core.Dtos.Query;
using OrderService.Core.Extensions;
using OrderService.Core.Models;

namespace OrderService.Application.Orders.Query.GetOrdersWithPagination;

public class GetOrdersWithPaginationHandler : IQueryHandler<PagedList<OrderDto>, GetOrdersWithPaginationQuery>
{
    private readonly IOrdersReadDbContext _readDbContext;

    public GetOrdersWithPaginationHandler(IOrdersReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<OrderDto>> Handle(
        GetOrdersWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var orderQuery = _readDbContext.Orders;

        return await orderQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}