using OrderService.Application.Orders.Query.GetOrdersWithPagination;

namespace OrderService.Presentation.Requests;

public record GetOrdersWithPaginationRequest(int Page, int PageSize)
{
    public GetOrdersWithPaginationQuery ToQuery()
        => new(Page, PageSize);
}