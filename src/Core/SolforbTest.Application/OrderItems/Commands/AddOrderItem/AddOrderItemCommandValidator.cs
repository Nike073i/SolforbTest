using FluentValidation;
using SolforbTest.Application.OrderItems.Validators;

namespace SolforbTest.Application.OrderItems.Commands.AddOrderItem
{
    public class AddOrderItemCommandValidator : AbstractValidator<AddOrderItemCommand>
    {
        public AddOrderItemCommandValidator(OrderItemDtoValidator orderItemDtoValidator)
        {
            RuleFor(command => command.OrderId).GreaterThan(0);
            RuleFor(command => command.OrderItemDto).SetValidator(orderItemDtoValidator);
        }
    }
}
