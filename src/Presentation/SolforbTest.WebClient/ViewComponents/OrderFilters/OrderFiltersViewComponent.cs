using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SolforbTest.Application.Orders.Queries.GetOrderItemNames;
using SolforbTest.Application.Orders.Queries.GetOrderItemUnits;
using SolforbTest.Application.Orders.Queries.GetOrderNumbers;
using SolforbTest.Application.Providers.Queries.GetProviderList;
using SolforbTest.WebClient.Models;
using System.Globalization;

namespace SolforbTest.WebClient.ViewComponents.OrderFilters
{
    public class OrderFiltersViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public OrderFiltersViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        private static FilterSelectViewModel CreateNamesFilterViewModel(
            IEnumerable<string> values,
            IEnumerable<string> selectedValues
        ) => new("Фильтр по наименованиям", "OrderItemNames", values, selectedValues);

        private static FilterSelectViewModel CreateUnitsFilterViewModel(
            IEnumerable<string> values,
            IEnumerable<string> selectedValues
        ) => new("Фильтр по величинам", "OrderItemUnits", values, selectedValues);

        private static FilterSelectViewModel CreateNumbersFilterViewModel(
            IEnumerable<string> values,
            IEnumerable<string> selectedValues
        ) => new("Фильтр по номерам", "OrderNumbers", values, selectedValues);

        private static FilterSelectViewModel CreateProvidersFilterViewModel(
            IEnumerable<ProviderViewModel> providers,
            IEnumerable<int> selectedValues
        ) =>
            new(
                "Фильтр по поставщикам",
                "ProviderIds",
                providers.Select(
                    p => new SelectListItem(p.Name, p.Id.ToString(CultureInfo.InvariantCulture))
                ),
                selectedValues.Select(pId => pId.ToString(CultureInfo.InvariantCulture))
            );

        private static FilterDateViewModel CreatePeriodStartFilterViewModel(
            DateTime? periodStart
        ) =>
            new("Дата начала периода", "PeriodStart", periodStart ?? DateTime.UtcNow.AddMonths(-1));

        private static FilterDateViewModel CreatPeriodEndFilterViewModel(DateTime? periodEnd) =>
            new("Дата окончания периода", "PeriodEnd", periodEnd ?? DateTime.UtcNow);

        public async Task<IViewComponentResult> InvokeAsync(
            IEnumerable<string> orderItemUnitsFilter,
            IEnumerable<string> orderItemNamesFilter,
            IEnumerable<string> orderNumbersFilter,
            IEnumerable<int> providerIdsFilter,
            DateTime? PeriodStart = null,
            DateTime? PeriodEnd = null
        )
        {
            // WhenAll не подойдет, т.к DbContext однопоточный
            var orderItemNames = await _mediator.Send(new GetOrderItemNamesQuery());
            var orderItemUnits = await _mediator.Send(new GetOrderItemUnitsQuery());
            var orderNumbers = await _mediator.Send(new GetOrderNumbersQuery());
            var providerViewModels = await _mediator.Send(new GetProviderListQuery());

            return View(
                "_OrderFiltersComponent",
                new OrderFiltersComponentViewModel(
                    CreateNamesFilterViewModel(orderItemNames, orderItemNamesFilter),
                    CreateUnitsFilterViewModel(orderItemUnits, orderItemUnitsFilter),
                    CreateNumbersFilterViewModel(orderNumbers, orderNumbersFilter),
                    CreateProvidersFilterViewModel(providerViewModels.Providers, providerIdsFilter),
                    CreatePeriodStartFilterViewModel(PeriodStart),
                    CreatPeriodEndFilterViewModel(PeriodEnd)
                )
            );
        }
    }
}
