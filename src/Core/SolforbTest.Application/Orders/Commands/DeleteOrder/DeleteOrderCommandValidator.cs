using FluentValidation;

namespace SolforbTest.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(command => command.OrderId).GreaterThan(0);
        }
    }
}
