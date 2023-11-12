using FluentValidation.TestHelper;
using SolforbTest.Application.Orders.Commands.DeleteOrder;

namespace SolforbTest.Core.Tests.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandValidatorTests
    {
        private readonly DeleteOrderCommandValidator _validator;

        public DeleteOrderCommandValidatorTests()
        {
            _validator = new DeleteOrderCommandValidator();
        }

        [Fact]
        public void DeleteOrderCommandValidator_Success()
        {
            int orderId = 1;
            var command = new DeleteOrderCommand(orderId);
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void DeleteOrderCommandValidator_FailOnWrongId()
        {
            int orderId = 0;
            var command = new DeleteOrderCommand(orderId);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(q => q.OrderId);
        }
    }
}
