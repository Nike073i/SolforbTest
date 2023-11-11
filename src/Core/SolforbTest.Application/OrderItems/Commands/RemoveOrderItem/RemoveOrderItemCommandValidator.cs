using FluentValidation;

namespace SolforbTest.Application.OrderItems.Commands.RemoveOrderItem
{
    public class RemoveOrderItemCommandValidator : AbstractValidator<RemoveOrderItemCommand>
    {
        public RemoveOrderItemCommandValidator()
        {
            RuleFor(command => command.OrderId).GreaterThan(0);
            RuleFor(command => command.OrderItemId).GreaterThan(0);
        }
    }
}
