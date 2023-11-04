using MediatR;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Application.Interfaces;

namespace SolforbTest.Application.Providers.Queries.GetProviderList
{
    public class GetProviderListQueryHandler
        : IRequestHandler<GetProviderListQuery, NoteListViewModel>
    {
        private readonly ISolforbDbContext _dbContext;

        public GetProviderListQueryHandler(ISolforbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<NoteListViewModel> Handle(
            GetProviderListQuery request,
            CancellationToken cancellationToken
        )
        {
            var storedProviders = await _dbContext.Providers.ToListAsync();
            var models = storedProviders.Select(
                provider => new NoteViewModel(provider.Id, provider.Name)
            );
            return new(models);
        }
    }
}
