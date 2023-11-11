using FluentValidation;

namespace SolforbTest.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(command => command.OrderId).GreaterThan(0);
            RuleFor(command => command.Number).NotEmpty();
            RuleFor(command => command.Date)
                .InclusiveBetween(new DateTime(2000, 1, 1), new DateTime(2100, 1, 1));
            RuleFor(command => command.ProviderId).GreaterThan(0);
        }
    }
}
