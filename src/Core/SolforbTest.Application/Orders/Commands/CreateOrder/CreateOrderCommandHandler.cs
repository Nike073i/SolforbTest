using MediatR;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Application.Common.Exceptions;
using SolforbTest.Application.Interfaces;
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
            var provider =
                await _dbContext.Providers.FirstOrDefaultAsync(
                    p => p.Id == request.ProviderId,
                    cancellationToken
                ) ?? throw new NotFoundException("Provider", request.ProviderId);

            bool isExistsOrder = await _dbContext.Orders.AnyAsync(
                o => o.Number == request.Number && o.ProviderId == request.ProviderId,
                cancellationToken
            );
            if (isExistsOrder)
                throw new AlreadyExistException(
                    $"Заказ с номером - {request.Number} у поставщика с Id - {request.ProviderId} уже существует"
                );
            var order = new Order(request.Number, DateTime.UtcNow, request.ProviderId)
            {
                OrderItems = request.OrderItems
                    .Select(dto => new OrderItem(dto.Name, dto.Quantity, dto.Unit))
                    .ToList()
            };
            await _dbContext.Orders.AddAsync(order, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return order.Id;
        }
    }
}
