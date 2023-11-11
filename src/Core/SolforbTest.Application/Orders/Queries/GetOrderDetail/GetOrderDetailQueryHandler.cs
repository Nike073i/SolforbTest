using MediatR;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Application.Common.Extensions;
using SolforbTest.Application.Interfaces;
using SolforbTest.Application.OrderItems.Dto;

namespace SolforbTest.Application.Orders.Queries.GetOrderDetail
{
    public class GetOrderDetailQueryHandler
        : IRequestHandler<GetOrderDetailQuery, OrderDetailViewModel>
    {
        private readonly ISolforbDbContext _dbContext;

        public GetOrderDetailQueryHandler(ISolforbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderDetailViewModel> Handle(
            GetOrderDetailQuery request,
            CancellationToken cancellationToken
        )
        {
            int orderId = request.OrderId;
            var order = await _dbContext.Orders
                .AsNoTracking()
                .Include(o => o.OrderItems)
                .Include(o => o.Provider)
                .GetByIdOrThrow(orderId, cancellationToken);

            return new OrderDetailViewModel(
                orderId,
                order.Number,
                order.Date,
                order.Provider!.Name,
                order.ProviderId,
                order.OrderItems!.Select(
                    item => new OrderItemViewModel(item.Id, item.Name, item.Quantity, item.Unit)
                )
            );
        }
    }
}
