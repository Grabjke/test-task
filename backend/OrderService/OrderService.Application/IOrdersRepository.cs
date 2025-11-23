using System.Linq.Expressions;
using CSharpFunctionalExtensions;
using OrderService.Domain.ItemManagement;
using OrderService.SharedKernel;

namespace OrderService.Application;

public interface IOrdersRepository
{
    public Task<Guid> Add(
        Order order,
        CancellationToken cancellationToken = default);

    public Task<Result<Order, Error>> GetBy(
        Expression<Func<Order, bool>> predicate,
        CancellationToken cancellationToken = default);
    
    public Task<Guid> Save(
        Order order,
        CancellationToken cancellationToken = default);

    Task<Guid> Delete(
        Order order,
        CancellationToken cancellationToken = default);
}