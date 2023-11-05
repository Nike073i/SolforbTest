using Microsoft.EntityFrameworkCore;
using SolforbTest.Application.Common.Exceptions;
using SolforbTest.Domain;

namespace SolforbTest.Application.Orders.Helpers
{
    public static class OrderExtensions
    {
        public static async Task<Order> GetOrThrowException(
            this IQueryable<Order> queryable,
            int orderId,
            CancellationToken cancellationToken = default
        ) =>
            await queryable.FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken)
            ?? throw new NotFoundException("Order", orderId);

        public static Task<bool> HaveProviderOrder(
            this IQueryable<Order> queryable,
            int providerId,
            string number,
            CancellationToken cancellationToken = default
        ) =>
            queryable.AnyAsync(
                o => o.Number == number && o.ProviderId == providerId,
                cancellationToken
            );
    }
}
