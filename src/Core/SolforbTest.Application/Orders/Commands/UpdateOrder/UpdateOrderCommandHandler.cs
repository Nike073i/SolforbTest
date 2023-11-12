using MediatR;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Application.Common.Exceptions;
using SolforbTest.Application.Common.Extensions;
using SolforbTest.Application.Interfaces;
using SolforbTest.Application.Orders.Helpers;

namespace SolforbTest.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, int>
    {
        private readonly ISolforbDbContext _dbContext;

        public UpdateOrderCommandHandler(ISolforbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(
            UpdateOrderCommand request,
            CancellationToken cancellationToken
        )
        {
            (int orderId, string newNumber, var newDate, int newProviderId) = request;
            var order = await _dbContext.Orders
                .Include(o => o.OrderItems)
                .GetByIdOrThrow(orderId, cancellationToken);

            order.Date = newDate;
            order.ProviderId = newProviderId;
            order.Number = newNumber;

            if (order.OrderItems!.Any(item => item.Name == order.Number))
            {
                throw new InvalidOrderNumberException(
                    $"Номер заказа - \"{order.Number}\" совпадает с названием элемента заказа"
                );
            }
            bool orderAlreadyExists = await _dbContext.Orders.DoesProviderAlreadyHaveOrder(
                order.ProviderId,
                order.Number,
                orderId,
                cancellationToken
            );
            if (orderAlreadyExists)
            {
                throw new AlreadyExistException(
                    $"Заказ с номером - {newNumber} у поставщика с Id - {request.ProviderId} уже существует"
                );
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return orderId;
        }
    }
}
