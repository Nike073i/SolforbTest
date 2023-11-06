using MediatR;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Application.Interfaces;

namespace SolforbTest.Application.Orders.Queries.GetOrderNumbers
{
    public class GetOrderNumbersQueryHandler
        : IRequestHandler<GetOrderNumbersQuery, IEnumerable<string>>
    {
        private readonly ISolforbDbContext _dbContext;

        public GetOrderNumbersQueryHandler(ISolforbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<string>> Handle(
            GetOrderNumbersQuery request,
            CancellationToken cancellationToken
        ) =>
            await _dbContext.Orders.Select(o => o.Number).Distinct().ToListAsync(cancellationToken);
    }
}
