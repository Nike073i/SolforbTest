using FluentValidation;
using SolforbTest.Application.OrderItems.Dto;

namespace SolforbTest.Application.OrderItems.Validators
{
    public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemDtoValidator()
        {
            RuleFor(item => item.Name).NotEmpty();
            RuleFor(item => item.Quantity).GreaterThan(0);
            RuleFor(item => item.Unit).NotEmpty();
        }
    }
}
