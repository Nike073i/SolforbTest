using FluentValidation.TestHelper;
using SolforbTest.Application.OrderItems.Commands.AddOrderItem;
using SolforbTest.Application.OrderItems.Dto;
using SolforbTest.Application.OrderItems.Validators;

namespace SolforbTest.Core.Tests.OrderItems.Commands.AddOrderItem
{
    public class AddOrderItemCommandValidatorTests
    {
        private readonly AddOrderItemCommandValidator _validator;

        public AddOrderItemCommandValidatorTests()
        {
            var orderItemValidator = new OrderItemDtoValidator();
            _validator = new AddOrderItemCommandValidator(orderItemValidator);
        }

        [Fact]
        public void AddOrderItemCommandValidator_Success()
        {
            var command = CreateCommand();
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void AddOrderItemCommandValidator_FailOnWrongOrderId()
        {
            var command = CreateCommand(orderId: 0);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.OrderId);
        }

        private AddOrderItemCommand CreateCommand(int orderId = 1)
        {
            var orderItemDto = new OrderItemDto("Name", 15m, "Unit");
            return new AddOrderItemCommand(orderId, orderItemDto);
        }
    }
}
