using MediatR;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Application.Common.Exceptions;
using SolforbTest.Application.Interfaces;

namespace SolforbTest.Application.Orders.Commands.RemoveOrderItem
{
    public class RemoveOrderItemCommandHandler : IRequestHandler<RemoveOrderItemCommand, Unit>
    {
        private readonly ISolforbDbContext _dbContext;

        public RemoveOrderItemCommandHandler(ISolforbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(
            RemoveOrderItemCommand request,
            CancellationToken cancellationToken
        )
        {
            (int orderId, int orderItemId) = request;
            var order =
                await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken)
                ?? throw new NotFoundException("Order", orderId);

            await _dbContext.Orders
                .Entry(order)
                .Collection(o => o.OrderItems!)
                .LoadAsync(cancellationToken);

            if (order.OrderItems?.Count <= 1)
            {
                throw new RemoveForbiddenException(
                    "OrderItem",
                    orderItemId,
                    "Заказ не может не содержать элементов"
                );
            }

            var orderItem =
                order.OrderItems?.FirstOrDefault(item => item.Id == orderItemId)
                ?? throw new NotFoundException("OrderItem", orderItemId);

            order.OrderItems.Remove(orderItem);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}