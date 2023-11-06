using FluentValidation;
using SolforbTest.Application.Orders.Validators;

namespace SolforbTest.Application.Orders.Commands.AddOrderItem
{
    public class AddOrderItemCommandValidator : AbstractValidator<AddOrderItemCommand>
    {
        public AddOrderItemCommandValidator(CreateOrderItemDtoValidator orderItemDtoValidator)
        {
            RuleFor(command => command.OrderId).GreaterThan(0);
            RuleFor(command => command.OrderItemDto).SetValidator(orderItemDtoValidator);
        }
    }
}
