using FluentValidation;

namespace SolforbTest.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(command => command.Number).NotEmpty();
            RuleFor(command => command.ProviderId).GreaterThan(0);
        }
    }
}
