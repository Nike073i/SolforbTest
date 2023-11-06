using MediatR;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Application.Interfaces;

namespace SolforbTest.Application.Orders.Queries.GetOrderItemNames
{
    public class GetOrderItemNamesQueryHandler
        : IRequestHandler<GetOrderItemNamesQuery, IEnumerable<string>>
    {
        private readonly ISolforbDbContext _dbContext;

        public GetOrderItemNamesQueryHandler(ISolforbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<string>> Handle(
            GetOrderItemNamesQuery request,
            CancellationToken cancellationToken
        ) =>
            await _dbContext.OrderItems
                .Select(item => item.Name)
                .Distinct()
                .ToListAsync(cancellationToken);
    }
}
