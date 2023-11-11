using SolforbTest.WebClient.Models.BindingModels;

namespace SolforbTest.WebClient.Models.ViewModels
{
    public record OrderFiltersComponentViewModel(
        MultipleSelectFilterBindingModel OrderItemNames,
        MultipleSelectFilterBindingModel OrderItemUnits,
        MultipleSelectFilterBindingModel OrderNumbers,
        MultipleSelectFilterBindingModel Providers,
        DateFilterBindingModel PeriodStart,
        DateFilterBindingModel PeriodEnd
    );
}
