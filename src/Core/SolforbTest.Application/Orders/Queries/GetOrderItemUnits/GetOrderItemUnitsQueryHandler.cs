using MediatR;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Application.Interfaces;

namespace SolforbTest.Application.Orders.Queries.GetOrderItemUnits
{
    public class GetOrderItemUnitsQueryHandler
        : IRequestHandler<GetOrderItemUnitsQuery, IEnumerable<string>>
    {
        private readonly ISolforbDbContext _dbContext;

        public GetOrderItemUnitsQueryHandler(ISolforbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<string>> Handle(
            GetOrderItemUnitsQuery request,
            CancellationToken cancellationToken
        ) =>
            await _dbContext.OrderItems
                .Select(item => item.Unit)
                .Distinct()
                .ToListAsync(cancellationToken);
    }
}
