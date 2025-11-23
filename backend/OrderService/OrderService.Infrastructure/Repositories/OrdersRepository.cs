using System.Linq.Expressions;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OrderService.Application;
using OrderService.Domain.ItemManagement;
using OrderService.Infrastructure.DbContexts;
using OrderService.SharedKernel;

namespace OrderService.Infrastructure.Repositories;

public class OrdersRepository : IOrdersRepository
{
    private readonly WriteOrderDbContext _context;

    public OrdersRepository(WriteOrderDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Add(Order order, CancellationToken cancellationToken = default)
    {
        await _context.Orders.AddAsync(order, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return order.Id;
    }

    public async Task<Result<Order, Error>> GetBy(
        Expression<Func<Order, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        var record = await _context.Orders
            .Include(v => v.Items)
            .FirstOrDefaultAsync(predicate, cancellationToken);

        if (record is null)
            return Errors.General.NotFound();

        return record;
    }

    public async Task<Guid> Save(Order order, CancellationToken cancellationToken = default)
    {
        _context.Orders.Attach(order);

        await _context.SaveChangesAsync(cancellationToken);

        return order.Id;
    }

    public async Task<Guid> Delete(Order order, CancellationToken cancellationToken = default)
    {
        _context.Remove(order);

        await _context.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}