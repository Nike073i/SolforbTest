using FluentValidation.TestHelper;
using SolforbTest.Application.Orders.Commands.CreateOrder;

namespace SolforbTest.Core.Tests.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidatorTests
    {
        private readonly CreateOrderCommandValidator _validator;

        public CreateOrderCommandValidatorTests()
        {
            _validator = new CreateOrderCommandValidator();
        }

        [Fact]
        public void CreateOrderCommandValidator_Success()
        {
            string number = Guid.NewGuid().ToString();
            int providerId = 1;

            var command = new CreateOrderCommand(number, providerId);
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void CreateOrderCommandValidator_FailOnEmptyNumber()
        {
            string number = string.Empty;
            int providerId = 1;
            var command = new CreateOrderCommand(number, providerId);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(q => q.Number);
        }

        [Fact]
        public void CreateOrderCommandValidator_FailOnWrongProviderId()
        {
            string number = Guid.NewGuid().ToString();
            int providerId = 0;
            var command = new CreateOrderCommand(number, providerId);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(q => q.ProviderId);
        }
    }
}
