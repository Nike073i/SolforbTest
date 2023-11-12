using MediatR;
using SolforbTest.Application.Common.Exceptions;
using SolforbTest.Application.Common.Extensions;
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
            (string number, int providerId) = request;
            await _dbContext.Providers.ThrowIfDoesntExist(providerId, cancellationToken);

            bool orderExists = await _dbContext.Orders.DoesProviderAlreadyHaveOrder(
                providerId,
                number,
                cancellationToken: cancellationToken
            );

            if (orderExists)
            {
                throw new AlreadyExistException(
                    $"Заказ с номером - {number} у поставщика с Id - {providerId} уже существует"
                );
            }

            var order = new Order(number, DateTime.UtcNow, providerId);
            await _dbContext.Orders.AddAsync(order, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return order.Id;
        }
    }
}
