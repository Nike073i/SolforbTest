using MediatR;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Application.Common.Extensions;
using SolforbTest.Application.Common.Options;
using SolforbTest.Application.Interfaces;
using SolforbTest.Application.Orders.Helpers;
using SolforbTest.Application.Orders.Options;
using SolforbTest.Domain;

namespace SolforbTest.Application.Orders.Queries.GetOrderList
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, OrderListViewModel>
    {
        private readonly ISolforbDbContext _dbContext;

        public GetOrderListQueryHandler(ISolforbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderListViewModel> Handle(
            GetOrderListQuery request,
            CancellationToken cancellationToken
        )
        {
            (var filterOptions, var paginationOptions, var sortOptions) = request;
            IQueryable<Order> orders = _dbContext.Orders
                .Include(o => o.Provider)
                .Include(o => o.OrderItems);

            orders = orders.Filter(filterOptions);

            paginationOptions ??= new PaginationOptions();
            orders = orders.Page(paginationOptions);

            sortOptions ??= new OrderSortOptions();
            orders = orders.Sort(sortOptions);

            var ordersViewModel = await orders
                .Select(o => new OrderViewModel(o.Id, o.Number, o.Date, o.Provider!.Name))
                .ToListAsync(cancellationToken);

            return new OrderListViewModel(
                paginationOptions.PageNumber,
                (int)Math.Ceiling((double)orders.Count() / paginationOptions.PageSize),
                ordersViewModel
            );
        }
    }
}
