using MediatR;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Application.Common.Extensions;
using SolforbTest.Application.Interfaces;
using SolforbTest.Application.OrderItems.Dto;

namespace SolforbTest.Application.OrderItems.Queries.GetOrderItem
{
    public class GetOrderItemQueryHandler : IRequestHandler<GetOrderItemQuery, OrderItemViewModel>
    {
        private readonly ISolforbDbContext _dbContext;

        public GetOrderItemQueryHandler(ISolforbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderItemViewModel> Handle(
            GetOrderItemQuery request,
            CancellationToken cancellationToken
        )
        {
            var orderItem = await _dbContext.OrderItems
                .AsNoTracking()
                .GetByIdOrThrow(request.OrderItemId, cancellationToken);
            return new OrderItemViewModel(
                orderItem.Id,
                orderItem.Name,
                orderItem.Quantity,
                orderItem.Unit
            );
        }
    }
}
