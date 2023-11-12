using FluentValidation.TestHelper;
using SolforbTest.Application.OrderItems.Commands.RemoveOrderItem;

namespace SolforbTest.Core.Tests.OrderItems.Commands.RemoveOrderItem
{
    public class RemoveOrderItemCommandValidatorTests
    {
        private readonly RemoveOrderItemCommandValidator _validator;

        public RemoveOrderItemCommandValidatorTests()
        {
            _validator = new RemoveOrderItemCommandValidator();
        }

        [Fact]
        public void RemoveOrderItemCommandValidator_Success()
        {
            var command = CreateCommand();
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void RemoveOrderItemCommandValidator_FailOnWrongOrderId()
        {
            var command = CreateCommand(orderId: 0);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.OrderId);
        }

        [Fact]
        public void RemoveOrderItemCommandValidator_FailOnWrongOrderItemId()
        {
            var command = CreateCommand(orderItemId: 0);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.OrderItemId);
        }

        private RemoveOrderItemCommand CreateCommand(int orderId = 1, int orderItemId = 1001) =>
            new RemoveOrderItemCommand(orderId, orderItemId);
    }
}
