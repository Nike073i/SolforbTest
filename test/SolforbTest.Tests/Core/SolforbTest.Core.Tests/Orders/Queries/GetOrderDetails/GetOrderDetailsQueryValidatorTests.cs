using FluentValidation.TestHelper;
using SolforbTest.Application.Orders.Queries.GetOrderDetail;

namespace SolforbTest.Core.Tests.Orders.Queries.GetOrderDetails
{
    public class GetOrderDetailsQueryValidatorTests
    {
        private readonly GetOrderDetailQueryValidator _validator;

        public GetOrderDetailsQueryValidatorTests()
        {
            _validator = new GetOrderDetailQueryValidator();
        }

        [Fact]
        public void GetOrderDetailQueryValidator_Success()
        {
            int orderId = 1;
            var query = new GetOrderDetailQuery(orderId);
            var result = _validator.TestValidate(query);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void GetOrderDetailQueryValidator_FailOnWrongId()
        {
            int orderId = 0;
            var query = new GetOrderDetailQuery(orderId);
            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(q => q.OrderId);
        }
    }
}
