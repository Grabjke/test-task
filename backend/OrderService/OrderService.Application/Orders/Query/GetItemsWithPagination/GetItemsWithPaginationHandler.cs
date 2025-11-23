using System.Linq.Expressions;
using OrderService.Core;
using OrderService.Core.Abstractions;
using OrderService.Core.Dtos.Query;
using OrderService.Core.Extensions;
using OrderService.Core.Models;

namespace OrderService.Application.Orders.Query.GetItemsWithPagination;

public class GetItemsWithPaginationHandler : IQueryHandler<PagedList<ItemDto>, GetItemsWithPaginationQuery>
{
    private readonly IOrdersReadDbContext _readDbContext;

    public GetItemsWithPaginationHandler(IOrdersReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public Task<PagedList<ItemDto>> Handle(
        GetItemsWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var itemsQuery = _readDbContext.Items;

        itemsQuery = itemsQuery.WhereIf(
            query.OrderId.HasValue,
            o => o.OrderId == query.OrderId);

        itemsQuery = itemsQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.Description),
            o => o.Description.Contains(query.Description!));

        itemsQuery = itemsQuery.WhereIf(
            query.Price.HasValue,
            o => o.PriceAmount == query.Price);

        itemsQuery = itemsQuery.WhereIf(
            query.Quantity.HasValue,
            o => o.Quantity == query.Quantity);

        itemsQuery = itemsQuery.WhereIf(
            !string.IsNullOrWhiteSpace(query.Name),
            o => o.Name.Contains(query.Name!));

        itemsQuery = itemsQuery.WhereIf(
            query.Discount.HasValue,
            o => o.DiscountValue == query.Discount);

        Expression<Func<ItemDto, object>> keySelector = query.SortBy?.ToLower()switch
        {
            "name" => x => x.Name,
            "description" => x => x.Description,
            "price" => x => x.PriceAmount,
            "quantity" => x => x.Quantity,
            "discount" => x => x.DiscountValue,
            _ => x => x.Id
        };

        itemsQuery = query.SortDirection?.ToLower() == "desc"
            ? itemsQuery.OrderByDescending(keySelector)
            : itemsQuery.OrderBy(keySelector);

        return itemsQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}