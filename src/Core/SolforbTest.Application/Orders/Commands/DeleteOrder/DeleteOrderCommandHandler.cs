using MediatR;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Application.Common.Exceptions;
using SolforbTest.Application.Interfaces;

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
            var order =
                await _dbContext.Orders.FirstOrDefaultAsync(
                    o => o.Id == request.OrderId,
                    cancellationToken
                ) ?? throw new NotFoundException("Order", request.OrderId);
            await _dbContext.Orders
                .Entry(order)
                .Collection(o => o.OrderItems!)
                .LoadAsync(cancellationToken);
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return request.OrderId;
        }
    }
}
