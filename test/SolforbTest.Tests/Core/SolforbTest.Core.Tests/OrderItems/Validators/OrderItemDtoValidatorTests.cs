using FluentValidation.TestHelper;
using SolforbTest.Application.OrderItems.Dto;
using SolforbTest.Application.OrderItems.Validators;

namespace SolforbTest.Core.Tests.OrderItems.Validators
{
    public class OrderItemDtoValidatorTests
    {
        private readonly OrderItemDtoValidator _validator;

        public OrderItemDtoValidatorTests()
        {
            _validator = new OrderItemDtoValidator();
        }

        [Fact]
        public void OrderItemDtoValidator_Success()
        {
            var dto = CreateDto();
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void OrderItemDtoValidator_FailOnEmptyName()
        {
            var dto = CreateDto(name: string.Empty);
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(dto => dto.Name);
        }

        [Fact]
        public void OrderItemDtoValidator_FailOnWrongQuantity()
        {
            var dto = CreateDto(quantity: -1);
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(dto => dto.Quantity);
        }

        [Fact]
        public void OrderItemDtoValidator_FailOnEmptyUnit()
        {
            var dto = CreateDto(unit: string.Empty);
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(dto => dto.Unit);
        }

        private static OrderItemDto CreateDto(string name = "Name", decimal quantity = 123.4m, string unit = "kg") => new(name, quantity, unit);
    }
}
