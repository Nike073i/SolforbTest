using FluentValidation.TestHelper;
using SolforbTest.Application.OrderItems.Queries.GetOrderItem;

namespace SolforbTest.Core.Tests.OrderItems.Queries.GetOrderItem
{
    public class GetOrderItemQueryValidatorTests
    {
        private readonly GetOrderItemQueryValidator _validator;

        public GetOrderItemQueryValidatorTests()
        {
            _validator = new GetOrderItemQueryValidator();
        }

        [Fact]
        public void GetOrderItemQueryValidator_Success()
        {
            int orderItemId = 1;
            var query = new GetOrderItemQuery(orderItemId);
            var result = _validator.TestValidate(query);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void GetOrderItemQueryValidator_FailOnWrongId()
        {
            int orderItemId = 0;
            var query = new GetOrderItemQuery(orderItemId);
            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(q => q.OrderItemId);
        }
    }
}
