using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SolforbTest.Application.OrderItems.Queries.GetOrderItemNames;
using SolforbTest.Application.OrderItems.Queries.GetOrderItemUnits;
using SolforbTest.Application.Orders.Queries.GetOrderNumbers;
using SolforbTest.Application.Providers.Queries.GetProviderList;
using SolforbTest.WebClient.Models.BindingModels;
using SolforbTest.WebClient.Models.ViewModels;
using System.Globalization;

namespace SolforbTest.WebClient.ViewComponents.Order
{
    public class OrderFiltersViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public OrderFiltersViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        private static MultipleSelectFilterBindingModel CreateNamesFilterModel(
            IEnumerable<string> values,
            IEnumerable<string> selectedValues
        ) => new("Фильтр по наименованиям", "OrderItemNames", values, selectedValues);

        private static MultipleSelectFilterBindingModel CreateUnitsFilterModel(
            IEnumerable<string> values,
            IEnumerable<string> selectedValues
        ) => new("Фильтр по величинам", "OrderItemUnits", values, selectedValues);

        private static MultipleSelectFilterBindingModel CreateNumbersFilterModel(
            IEnumerable<string> values,
            IEnumerable<string> selectedValues
        ) => new("Фильтр по номерам", "OrderNumbers", values, selectedValues);

        private static MultipleSelectFilterBindingModel CreateProvidersFilterModel(
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

        private static DateFilterBindingModel CreatePeriodStartFilterModel(DateTime? periodStart) =>
            new("Дата начала периода", "PeriodStart", periodStart ?? DateTime.Now.AddMonths(-1));

        private static DateFilterBindingModel CreatPeriodEndFilterModel(DateTime? periodEnd) =>
            new("Дата окончания периода", "PeriodEnd", periodEnd ?? DateTime.Now);

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
                "/Views/Shared/Order/_OrderFiltersComponent.cshtml",
                new OrderFiltersComponentViewModel(
                    CreateNamesFilterModel(orderItemNames, orderItemNamesFilter),
                    CreateUnitsFilterModel(orderItemUnits, orderItemUnitsFilter),
                    CreateNumbersFilterModel(orderNumbers, orderNumbersFilter),
                    CreateProvidersFilterModel(providerViewModels.Providers, providerIdsFilter),
                    CreatePeriodStartFilterModel(PeriodStart),
                    CreatPeriodEndFilterModel(PeriodEnd)
                )
            );
        }
    }
}
