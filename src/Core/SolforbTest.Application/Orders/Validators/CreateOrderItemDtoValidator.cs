using FluentValidation;
using SolforbTest.Application.Orders.Dto;

namespace SolforbTest.Application.Orders.Validators
{
    public class CreateOrderItemDtoValidator : AbstractValidator<CreateOrderItemDto>
    {
        public CreateOrderItemDtoValidator()
        {
            RuleFor(item => item.Name).NotEmpty();
            RuleFor(item => item.Quantity).GreaterThan(0);
            RuleFor(item => item.Unit).NotEmpty();
        }
    }
}
