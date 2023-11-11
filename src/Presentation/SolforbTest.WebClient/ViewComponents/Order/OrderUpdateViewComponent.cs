using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolforbTest.Application.Orders.Queries.GetOrderDetail;
using SolforbTest.Application.Providers.Queries.GetProviderList;
using SolforbTest.WebClient.Models.ViewModels;

namespace SolforbTest.WebClient.ViewComponents.Order
{
    public class OrderUpdateViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public OrderUpdateViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int orderId)
        {
            var details = await _mediator.Send(new GetOrderDetailQuery(orderId));
            var providers = await _mediator.Send(new GetProviderListQuery());
            return View(
                "/Views/Shared/Order/_OrderUpdateComponent.cshtml",
                new OrderUpdateComponentViewModel(details, providers.Providers, details.OrderItems)
            );
        }
    }
}
