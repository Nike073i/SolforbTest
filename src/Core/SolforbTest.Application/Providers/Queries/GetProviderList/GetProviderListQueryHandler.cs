using MediatR;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Application.Interfaces;

namespace SolforbTest.Application.Providers.Queries.GetProviderList
{
    public class GetProviderListQueryHandler
        : IRequestHandler<GetProviderListQuery, ProvidersListViewModel>
    {
        private readonly ISolforbDbContext _dbContext;

        public GetProviderListQueryHandler(ISolforbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProvidersListViewModel> Handle(
            GetProviderListQuery request,
            CancellationToken cancellationToken
        )
        {
            var storedProviders = await _dbContext.Providers
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            var models = storedProviders.Select(
                provider => new ProviderViewModel(provider.Id, provider.Name)
            );
            return new(models);
        }
    }
}
