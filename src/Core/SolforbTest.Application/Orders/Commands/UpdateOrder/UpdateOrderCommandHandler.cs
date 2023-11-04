using MediatR;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Application.Common.Exceptions;
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
            (int orderId, string? newNumber, int? newProviderId) = request;
            var order =
                await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId)
                ?? throw new NotFoundException("Order", orderId);

            order.ProviderId = newProviderId ?? order.ProviderId;
            order.Number = newNumber ?? order.Number;

            bool isExistsOrder = await _dbContext.Orders.HaveProviderOrder(
                order.ProviderId,
                order.Number,
                cancellationToken
            );
            if (isExistsOrder)
                throw new AlreadyExistException(
                    $"Заказ с номером - {newNumber} у поставщика с Id - {request.ProviderId} уже существует"
                );
            await _dbContext.SaveChangesAsync(cancellationToken);
            return orderId;
        }
    }
}
