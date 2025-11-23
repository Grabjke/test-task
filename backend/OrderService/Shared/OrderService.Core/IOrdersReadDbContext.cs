
using OrderService.Core.Dtos.Query;

namespace OrderService.Core;

public interface IOrdersReadDbContext
{
    public IQueryable<OrderDto> Orders { get; }
    public IQueryable<ItemDto> Items { get; }
}