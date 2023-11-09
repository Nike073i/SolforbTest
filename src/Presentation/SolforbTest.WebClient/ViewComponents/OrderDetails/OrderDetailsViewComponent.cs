using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolforbTest.Application.Orders.Queries.GetOrderDetail;
using SolforbTest.WebClient.Models;

namespace SolforbTest.WebClient.ViewComponents.OrderDetails
{
    public class OrderDetailsViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public OrderDetailsViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int orderId)
        {
            var orderDetailsViewModel = await _mediator.Send(new GetOrderDetailQuery(orderId));
            return View(
                "_OrderDetailsComponent",
                new OrderDetailsComponentViewModel(orderDetailsViewModel)
            );
        }
    }
}
