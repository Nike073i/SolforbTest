namespace SolforbTest.Application.Orders.Options
{
    public record OrderFilterOptions(
        DateTime? PeriodStart = null,
        DateTime? PeriodEnd = null,
        IEnumerable<int>? ProviderIds = null,
        IEnumerable<string>? Numbers = null,
        IEnumerable<string>? Names = null,
        IEnumerable<string>? Units = null
    );
}
