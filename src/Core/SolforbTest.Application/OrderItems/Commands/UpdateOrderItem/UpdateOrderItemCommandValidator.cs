using FluentValidation;
using SolforbTest.Application.OrderItems.Validators;

namespace SolforbTest.Application.OrderItems.Commands.UpdateOrderItem
{
    public class UpdateOrderItemCommandValidator : AbstractValidator<UpdateOrderItemCommand>
    {
        public UpdateOrderItemCommandValidator(OrderItemDtoValidator orderItemDtoValidator)
        {
            RuleFor(command => command.OrderId).GreaterThan(0);
            RuleFor(command => command.OrderItemId).GreaterThan(0);
            RuleFor(command => command.OrderItemDto).SetValidator(orderItemDtoValidator);
        }
    }
}
