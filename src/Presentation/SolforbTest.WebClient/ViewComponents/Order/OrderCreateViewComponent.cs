using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolforbTest.Application.Providers.Queries.GetProviderList;
using SolforbTest.WebClient.Models.ViewModels;

namespace SolforbTest.WebClient.ViewComponents.Order
{
    public class OrderCreateViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public OrderCreateViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var providers = await _mediator.Send(new GetProviderListQuery());
            return View(
                "/Views/Shared/Order/_OrderCreateComponent.cshtml",
                new OrderCreateComponentViewModel(providers.Providers)
            );
        }
    }
}
