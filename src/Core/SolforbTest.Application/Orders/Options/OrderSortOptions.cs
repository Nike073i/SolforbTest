using SolforbTest.Application.Common.Enums;
using SolforbTest.Application.Orders.Enums;

namespace SolforbTest.Application.Orders.Options
{
    public record OrderSortOptions(
        SortOrderField SortField = SortOrderField.Id,
        SortDirection SortDirection = SortDirection.Asc
    );
}
