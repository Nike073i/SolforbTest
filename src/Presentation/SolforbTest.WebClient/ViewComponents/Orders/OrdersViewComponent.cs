using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolforbTest.Application.Common.Options;
using SolforbTest.Application.Orders.Options;
using SolforbTest.Application.Orders.Queries.GetOrderList;
using SolforbTest.WebClient.Models;

namespace SolforbTest.WebClient.ViewComponents.Orders
{
    public class OrdersViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public OrdersViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(
            int pageSize,
            int pageNumber,
            IEnumerable<string> orderItemUnitsFilter,
            IEnumerable<string> orderItemNamesFilter,
            IEnumerable<string> orderNumbersFilter,
            IEnumerable<int> providerIdsFilter,
            DateTime? periodStart,
            DateTime? periodEnd
        )
        {
            var orderViewModels = await _mediator.Send(
                new GetOrderListQuery(
                    PaginationOptions: new PaginationOptions(pageSize, pageNumber),
                    FilterOptions: new OrderFilterOptions(
                        Units: orderItemUnitsFilter,
                        Names: orderItemNamesFilter,
                        Numbers: orderNumbersFilter,
                        ProviderIds: providerIdsFilter,
                        PeriodStart: periodStart,
                        PeriodEnd: periodEnd
                    )
                )
            );

            return View("_OrdersComponent", new OrdersComponentViewModel(orderViewModels));
        }
    }
}
