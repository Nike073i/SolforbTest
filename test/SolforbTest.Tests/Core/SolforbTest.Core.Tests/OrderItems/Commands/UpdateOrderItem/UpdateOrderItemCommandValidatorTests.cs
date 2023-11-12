using FluentValidation.TestHelper;
using SolforbTest.Application.OrderItems.Commands.UpdateOrderItem;
using SolforbTest.Application.OrderItems.Dto;
using SolforbTest.Application.OrderItems.Validators;

namespace SolforbTest.Core.Tests.OrderItems.Commands.UpdateOrderItem
{
    public class UpdateOrderItemCommandValidatorTests
    {
        private readonly UpdateOrderItemCommandValidator _validator;

        public UpdateOrderItemCommandValidatorTests()
        {
            var orderItemValidator = new OrderItemDtoValidator();
            _validator = new UpdateOrderItemCommandValidator(orderItemValidator);
        }

        [Fact]
        public void UpdateOrderItemCommandValidator_Success()
        {
            var command = CreateCommand();
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void UpdateOrderItemCommandValidator_FailOnWrongOrderId()
        {
            var command = CreateCommand(orderId: 0);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.OrderId);
        }

        [Fact]
        public void UpdateOrderItemCommandValidator_FailOnWrongOrderItemId()
        {
            var command = CreateCommand(orderItemId: 0);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.OrderItemId);
        }

        private UpdateOrderItemCommand CreateCommand(int orderId = 1, int orderItemId = 1001)
        {
            var orderItemDto = new OrderItemDto("Name", 15m, "Unit");
            return new UpdateOrderItemCommand(orderId, orderItemId, orderItemDto);
        }
    }
}
