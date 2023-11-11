using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolforbTest.Application.Common.Options;
using SolforbTest.Application.Orders.Options;
using SolforbTest.Application.Orders.Queries.GetOrderList;
using SolforbTest.WebClient.Models.ViewModels;

namespace SolforbTest.WebClient.ViewComponents.Order
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
            var orderList = await _mediator.Send(
                new GetOrderListQuery(
                    PaginationOptions: new PaginationOptions(pageSize, pageNumber),
                    FilterOptions: new OrderFilterOptions(
                        Units: orderItemUnitsFilter,
                        Names: orderItemNamesFilter,
                        Numbers: orderNumbersFilter,
                        ProviderIds: providerIdsFilter,
                        PeriodStart: periodStart?.ToUniversalTime(),
                        PeriodEnd: periodEnd?.ToUniversalTime()
                    )
                )
            );

            return View(
                "/Views/Shared/Order/_OrdersComponent.cshtml",
                new OrdersComponentViewModel(
                    orderList.Orders,
                    new PaginationViewModel(orderList.PageNumber, orderList.PageCount)
                )
            );
        }
    }
}
