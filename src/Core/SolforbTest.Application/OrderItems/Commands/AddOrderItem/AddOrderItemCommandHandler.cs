using MediatR;
using SolforbTest.Application.Common.Exceptions;
using SolforbTest.Application.Common.Extensions;
using SolforbTest.Application.Interfaces;
using SolforbTest.Domain;

namespace SolforbTest.Application.OrderItems.Commands.AddOrderItem
{
    public class AddOrderItemCommandHandler : IRequestHandler<AddOrderItemCommand, int>
    {
        private readonly ISolforbDbContext _dbContext;

        public AddOrderItemCommandHandler(ISolforbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(
            AddOrderItemCommand request,
            CancellationToken cancellationToken
        )
        {
            (int orderId, var orderItemDto) = request;
            var order = await _dbContext.Orders.GetByIdOrThrow(orderId, cancellationToken);

            if (order.Number == orderItemDto.Name)
            {
                throw new InvalidOrderNumberException(
                    $"Номер заказа - \"{order.Number}\" совпадает с названием элемента заказа"
                );
            }

            await _dbContext.Orders
                .Entry(order)
                .Collection(o => o.OrderItems!)
                .LoadAsync(cancellationToken);

            bool orderItemExists = order.OrderItems!.Any(item => item.Name == orderItemDto.Name);
            if (orderItemExists)
            {
                throw new AlreadyExistException(
                    $"Элемент заказа \"{orderItemDto.Name}\" уже существует в заказе ({orderId})"
                );
            }
            var newOrderItem = new OrderItem(
                orderItemDto.Name,
                orderItemDto.Quantity,
                orderItemDto.Unit
            );
            order.OrderItems!.Add(newOrderItem);

            await _dbContext.SaveChangesAsync(cancellationToken);
            return newOrderItem.Id;
        }
    }
}
