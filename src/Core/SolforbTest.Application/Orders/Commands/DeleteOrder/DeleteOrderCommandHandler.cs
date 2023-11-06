using MediatR;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Application.Interfaces;
using SolforbTest.Application.Orders.Helpers;

namespace SolforbTest.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, int>
    {
        private readonly ISolforbDbContext _dbContext;

        public DeleteOrderCommandHandler(ISolforbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(
            DeleteOrderCommand request,
            CancellationToken cancellationToken
        )
        {
            var order = await _dbContext.Orders
                .Include(o => o.OrderItems)
                .GetByIdOrThrow(request.OrderId, cancellationToken);

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return request.OrderId;
        }
    }
}
