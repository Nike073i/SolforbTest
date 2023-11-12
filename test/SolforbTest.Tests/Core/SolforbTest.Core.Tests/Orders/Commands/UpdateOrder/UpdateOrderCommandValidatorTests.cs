using FluentValidation.TestHelper;
using SolforbTest.Application.Orders.Commands.UpdateOrder;

namespace SolforbTest.Core.Tests.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidatorTests
    {
        private readonly UpdateOrderCommandValidator _validator;

        public UpdateOrderCommandValidatorTests()
        {
            _validator = new UpdateOrderCommandValidator();
        }

        [Fact]
        public void UpdateOrderCommandValidator_Success()
        {
            var command = CreateCommand();
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void UpdateOrderCommandValidator_FailOnWrongOrderId()
        {
            var command = CreateCommand(orderId: 0);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.OrderId);
        }

        [Fact]
        public void UpdateOrderCommandValidator_FailOnEmptyNumber()
        {
            var command = CreateCommand(number: string.Empty);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Number);
        }

        [Fact]
        public void UpdateOrderCommandValidator_FailOnWrongProviderId()
        {
            var command = CreateCommand(providerId: 0);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.ProviderId);
        }

        [Theory]
        [InlineData(1999, 1, 1)]
        [InlineData(2101, 1, 1)]
        public void UpdateOrderCommandValidator_FailOnWrongDate(int year, int month, int day)
        {
            var command = CreateCommand(date: new DateTime(year, month, day));
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Date);
        }

        private UpdateOrderCommand CreateCommand(
            int orderId = 1,
            string number = "Number",
            int providerId = 1,
            DateTime? date = null
        ) => new UpdateOrderCommand(orderId, number, date ?? DateTime.UtcNow, providerId);
    }
}
