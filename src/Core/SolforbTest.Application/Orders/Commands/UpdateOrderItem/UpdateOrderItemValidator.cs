using FluentValidation;

namespace SolforbTest.Application.Orders.Commands.UpdateOrderItem
{
    public class UpdateOrderItemValidator : AbstractValidator<UpdateOrderItemCommand>
    {
        public UpdateOrderItemValidator(UpdateOrderItemDtoValidator orderItemDtoValidator)
        {
            RuleFor(command => command.OrderId).GreaterThan(0);
            RuleFor(command => command.OrderItemDto).SetValidator(orderItemDtoValidator);
        }
    }
}
