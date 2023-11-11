using MediatR;
using SolforbTest.Application.Common.Exceptions;
using SolforbTest.Application.Common.Extensions;
using SolforbTest.Application.Interfaces;

namespace SolforbTest.Application.OrderItems.Commands.UpdateOrderItem
{
    public class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand, Unit>
    {
        private readonly ISolforbDbContext _dbContext;

        public UpdateOrderItemCommandHandler(ISolforbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(
            UpdateOrderItemCommand request,
            CancellationToken cancellationToken
        )
        {
            (int orderId, int orderItemId, (string newName, decimal newQuantity, string newUnit)) =
                request;
            var order = await _dbContext.Orders.GetByIdOrThrow(orderId, cancellationToken);

            await _dbContext.Orders
                .Entry(order)
                .Collection(o => o.OrderItems!)
                .LoadAsync(cancellationToken);

            var orderItem =
                order.OrderItems?.FirstOrDefault(o => o.Id == orderItemId)
                ?? throw new NotFoundException("OrderItem", orderItemId);

            orderItem.Name = newName;
            orderItem.Quantity = newQuantity;
            orderItem.Unit = newUnit;

            if (order.Number == orderItem.Name)
            {
                throw new InvalidOrderNumberException(
                    $"Номер заказа - \"{order.Number}\" совпадает с названием элемента заказа"
                );
            }

            bool orderItemExists = order.OrderItems.Any(
                item => item.Name == orderItem.Name && item.Id != orderItem.Id
            );
            if (orderItemExists)
            {
                throw new AlreadyExistException(
                    $"Элемент заказа \"{orderItem.Name}\" уже существует в заказе ({orderId})"
                );
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
