using MediatR;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Application.Common.Exceptions;
using SolforbTest.Application.Interfaces;
using SolforbTest.Application.Orders.Helpers;
using SolforbTest.Domain;

namespace SolforbTest.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly ISolforbDbContext _dbContext;

        public CreateOrderCommandHandler(ISolforbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(
            CreateOrderCommand request,
            CancellationToken cancellationToken
        )
        {
            (string number, int providerId, var orderItemDtos) = request;
            var provider =
                await _dbContext.Providers.FirstOrDefaultAsync(
                    p => p.Id == providerId,
                    cancellationToken
                ) ?? throw new NotFoundException("Provider", providerId);

            bool orderExists = await _dbContext.Orders.HaveProviderOrder(
                providerId,
                number,
                cancellationToken
            );

            if (orderExists)
            {
                throw new AlreadyExistException(
                    $"Заказ с номером - {number} у поставщика с Id - {providerId} уже существует"
                );
            }

            var order = new Order(number, DateTime.UtcNow, providerId)
            {
                OrderItems = orderItemDtos
                    .Select(dto => new OrderItem(dto.Name, dto.Quantity, dto.Unit))
                    .ToList()
            };
            await _dbContext.Orders.AddAsync(order, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return order.Id;
        }
    }
}
