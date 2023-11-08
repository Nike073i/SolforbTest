namespace SolforbTest.WebClient.Models
{
    public record OrderFiltersComponentViewModel(
        FilterSelectViewModel OrderItemNames,
        FilterSelectViewModel OrderItemUnits,
        FilterSelectViewModel OrderNumbers,
        FilterSelectViewModel Providers,
        FilterDateViewModel PeriodStart,
        FilterDateViewModel PeriodEnd
    );
}
