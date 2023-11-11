using Microsoft.EntityFrameworkCore;
using SolforbTest.Application.Common.Enums;
using SolforbTest.Application.Common.Extensions;
using SolforbTest.Application.Orders.Enums;
using SolforbTest.Application.Orders.Options;
using SolforbTest.Domain;
using System.Linq.Expressions;

namespace SolforbTest.Application.Orders.Helpers
{
    public static class OrderExtensions
    {
        public static Task<bool> DoesProviderAlreadyHaveOrder(
            this IQueryable<Order> queryable,
            int providerId,
            string number,
            CancellationToken cancellationToken = default
        ) =>
            queryable.AnyAsync(
                o => o.Number == number && o.ProviderId == providerId,
                cancellationToken
            );

        public static IQueryable<Order> Sort(
            this IQueryable<Order> queryable,
            OrderSortOptions orderSortOptions
        )
        {
            Expression<Func<Order, int>> sortFieldSelector = orderSortOptions.SortField switch
            {
                SortOrderField.Id => o => o.Id,
                _ => throw new InvalidOperationException(),
            };

            return orderSortOptions.SortDirection == SortDirection.Asc
                ? queryable.OrderBy(sortFieldSelector)
                : queryable.OrderByDescending(sortFieldSelector);
        }

        public static IQueryable<Order> Filter(
            this IQueryable<Order> queryable,
            OrderFilterOptions? orderFilterOptions
        )
        {
            return orderFilterOptions == null
                ? queryable
                : queryable
                    .FilterByDate(orderFilterOptions.PeriodStart, orderFilterOptions.PeriodEnd)
                    .FilterByOrderItemUnits(orderFilterOptions.Units)
                    .FilterByOrderItemNames(orderFilterOptions.Names)
                    .FilterByNumbers(orderFilterOptions.Numbers)
                    .FilterByProviders(orderFilterOptions.ProviderIds);
        }

        public static IQueryable<Order> FilterByDate(
            this IQueryable<Order> queryable,
            DateTime? PeriodStart,
            DateTime? PeriodEnd
        )
        {
            if (PeriodStart.HasValue)
            {
                queryable = queryable.Where(o => o.Date >= PeriodStart.Value);
            }
            if (PeriodEnd.HasValue)
            {
                queryable = queryable.Where(o => o.Date <= PeriodEnd.Value);
            }
            return queryable;
        }

        public static IQueryable<Order> FilterByOrderItemUnits(
            this IQueryable<Order> queryable,
            IEnumerable<string>? units
        )
        {
            return units.IsNullOrEmpty()
                ? queryable
                : queryable.Where(
                    o =>
                        o.OrderItems != null
                        && o.OrderItems.Any(item => units!.Any(unit => unit == item.Unit))
                // IntersectBy(names, item => item.Name).Any() - LINQ не вывозит такую операцию при фильтрации на стороне БД
                );
        }

        public static IQueryable<Order> FilterByOrderItemNames(
            this IQueryable<Order> queryable,
            IEnumerable<string>? names
        )
        {
            return names.IsNullOrEmpty()
                ? queryable
                : queryable.Where(
                    o =>
                        o.OrderItems != null
                        && o.OrderItems.Any(item => names!.Any(name => name == item.Name))
                // IntersectBy(names, item => item.Name).Any() - LINQ не вывозит такую операцию при фильтрации на стороне БД
                );
        }

        public static IQueryable<Order> FilterByNumbers(
            this IQueryable<Order> queryable,
            IEnumerable<string>? numbers
        )
        {
            return numbers.IsNullOrEmpty()
                ? queryable
                : queryable.Where(o => numbers!.Contains(o.Number));
        }

        public static IQueryable<Order> FilterByProviders(
            this IQueryable<Order> queryable,
            IEnumerable<int>? providerIds
        )
        {
            return providerIds.IsNullOrEmpty()
                ? queryable
                : queryable.Where(o => providerIds!.Any(id => id == o.ProviderId));
        }
    }
}
