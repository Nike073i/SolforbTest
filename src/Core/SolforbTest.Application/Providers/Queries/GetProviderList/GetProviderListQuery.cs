using MediatR;

namespace SolforbTest.Application.Providers.Queries.GetProviderList
{
    public record GetProviderListQuery : IRequest<NoteListViewModel>;
}
