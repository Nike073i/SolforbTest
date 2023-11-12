using SolforbTest.Application.Common.Options;
using SolforbTest.Application.Orders.Options;
using SolforbTest.Application.Orders.Queries.GetOrderList;
using SolforbTest.Core.Tests.Common;
using SolforbTest.EfContext.Context;

namespace SolforbTest.Core.Tests.Orders.Queries
{
    [Collection(nameof(QueryCollection))]
    public class GetOrderListQueryHandlerTests
    {
        private readonly SolforbDbContext _context;

        public GetOrderListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetOrderListQueryHandlerTests_GetAll_Success()
        {
            int pageSize = 2;
            int currentPage = 1;
            var paginationOptions = new PaginationOptions(pageSize, currentPage);

            var result = await InvokeQuery(
                new GetOrderListQuery(PaginationOptions: paginationOptions)
            );

            int expectedPageCount = (int)
                Math.Ceiling((double)TestData.OrderTestList.Count / pageSize);

            result.PageCount.ShouldBe(expectedPageCount);
            result.PageNumber.ShouldBe(currentPage);
            result.Orders.Count().ShouldBeLessThanOrEqualTo(pageSize);
        }

        [Fact]
        public async Task GetOrderListQueryHandlerTests_GetByNumberExists_Success()
        {
            var result = await InvokeQuery(
                new GetOrderListQuery(
                    FilterOptions: new OrderFilterOptions(Numbers: new string[] { "Number 1" })
                )
            );
            result.Orders.Count().ShouldBe(2);
        }

        [Fact]
        public async Task GetOrderListQueryHandlerTests_GetByNumberNonExists_Empty()
        {
            var result = await InvokeQuery(
                new GetOrderListQuery(
                    FilterOptions: new OrderFilterOptions(
                        Numbers: new string[] { Guid.NewGuid().ToString() }
                    )
                )
            );
            result.Orders.Count().ShouldBe(0);
        }

        [Fact]
        public async Task GetOrderListQueryHandlerTests_GetByNameExists_Success()
        {
            var result = await InvokeQuery(
                new GetOrderListQuery(
                    FilterOptions: new OrderFilterOptions(Names: new string[] { "Name 3" })
                )
            );
            result.Orders.Count().ShouldBe(2);
        }

        [Fact]
        public async Task GetOrderListQueryHandlerTests_GetByNameNonExists_Empty()
        {
            var result = await InvokeQuery(
                new GetOrderListQuery(
                    FilterOptions: new OrderFilterOptions(
                        Names: new string[] { Guid.NewGuid().ToString() }
                    )
                )
            );
            result.Orders.Count().ShouldBe(0);
        }

        [Fact]
        public async Task GetOrderListQueryHandlerTests_GetByUnitExists_Success()
        {
            var result = await InvokeQuery(
                new GetOrderListQuery(
                    FilterOptions: new OrderFilterOptions(Units: new string[] { "kg" })
                )
            );
            result.Orders.Count().ShouldBe(2);
        }

        [Fact]
        public async Task GetOrderListQueryHandlerTests_GetByUnitNonExists_Empty()
        {
            var result = await InvokeQuery(
                new GetOrderListQuery(
                    FilterOptions: new OrderFilterOptions(
                        Units: new string[] { Guid.NewGuid().ToString() }
                    )
                )
            );

            result.Orders.Count().ShouldBe(0);
        }

        [Fact]
        public async Task GetOrderListQueryHandlerTests_GetByPeriodStartExists_Success()
        {
            var result = await InvokeQuery(
                new GetOrderListQuery(
                    FilterOptions: new OrderFilterOptions(
                        PeriodStart: TestData.CreateDateTime("11/09/2023")
                    )
                )
            );
            result.Orders.Count().ShouldBe(3);
        }

        [Fact]
        public async Task GetOrderListQueryHandlerTests_GetByPeriodStartNonExists_Empty()
        {
            var result = await InvokeQuery(
                new GetOrderListQuery(
                    FilterOptions: new OrderFilterOptions(
                        PeriodStart: TestData.CreateDateTime("12/01/2023")
                    )
                )
            );
            result.Orders.Count().ShouldBe(0);
        }

        [Fact]
        public async Task GetOrderListQueryHandlerTests_GetByPeriodEndExists_Success()
        {
            var result = await InvokeQuery(
                new GetOrderListQuery(
                    FilterOptions: new OrderFilterOptions(
                        PeriodEnd: TestData.CreateDateTime("11/09/2023")
                    )
                )
            );
            result.Orders.Count().ShouldBe(3);
        }

        [Fact]
        public async Task GetOrderListQueryHandlerTests_GetByPeriodEndNonExists_Empty()
        {
            var result = await InvokeQuery(
                new GetOrderListQuery(
                    FilterOptions: new OrderFilterOptions(
                        PeriodEnd: TestData.CreateDateTime("10/09/2023")
                    )
                )
            );

            result.Orders.Count().ShouldBe(0);
        }

        [Fact]
        public async Task GetOrderListQueryHandlerTests_GetByIntervalExists_Success()
        {
            var result = await InvokeQuery(
                new GetOrderListQuery(
                    FilterOptions: new OrderFilterOptions(
                        PeriodStart: TestData.CreateDateTime("11/04/2023"),
                        PeriodEnd: TestData.CreateDateTime("11/09/2023")
                    )
                )
            );

            result.Orders.Count().ShouldBe(2);
        }

        [Fact]
        public async Task GetOrderListQueryHandlerTests_GetByIntervalNonExists_Empty()
        {
            var result = await InvokeQuery(
                new GetOrderListQuery(
                    FilterOptions: new OrderFilterOptions(
                        PeriodStart: TestData.CreateDateTime("12/04/2023"),
                        PeriodEnd: TestData.CreateDateTime("12/09/2023")
                    )
                )
            );

            result.Orders.Count().ShouldBe(0);
        }

        [Fact]
        public async Task GetOrderListQueryHandlerTests_GetByProviderIdExists_Success()
        {
            var result = await InvokeQuery(
                new GetOrderListQuery(
                    FilterOptions: new OrderFilterOptions(ProviderIds: new int[] { 1 })
                )
            );

            result.Orders.Count().ShouldBe(2);
        }

        [Fact]
        public async Task GetOrderListQueryHandlerTests_GetByProviderIdNonExists_Empty()
        {
            var result = await InvokeQuery(
                new GetOrderListQuery(
                    FilterOptions: new OrderFilterOptions(ProviderIds: new int[] { 10 })
                )
            );
            result.Orders.Count().ShouldBe(0);
        }

        private async Task<OrderListViewModel> InvokeQuery(GetOrderListQuery getOrderListQuery)
        {
            var handler = new GetOrderListQueryHandler(_context);
            var result = await handler.Handle(getOrderListQuery, CancellationToken.None);

            result.ShouldBeOfType<OrderListViewModel>();
            return result;
        }
    }
}
