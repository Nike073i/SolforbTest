using FluentValidation;
using SolforbTest.Application.Orders.Dto;
using SolforbTest.Application.Orders.Validators;

namespace SolforbTest.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator(CreateOrderItemDtoValidator orderItemDtoValidator)
        {
            RuleFor(command => command.Number)
                .NotEmpty()
                .Must(BeDifferentFromNumber)
                .WithMessage("Number не должен совпадать с именем ни одного элемента заказа");
            RuleFor(command => command.ProviderId).GreaterThan(0);
            RuleFor(command => command.OrderItems)
                .NotEmpty()
                .Must(BeUniqueNames)
                .WithMessage("Имена элементов заказа должны быть уникальными.");
            RuleForEach(command => command.OrderItems).SetValidator(orderItemDtoValidator);
        }

        private bool BeUniqueNames(IEnumerable<CreateOrderItemDto> items) =>
            items.Select(item => item.Name).Distinct().Count() == items.Count();

        private bool BeDifferentFromNumber(CreateOrderCommand command, string number) =>
            !command.OrderItems.Any(item => item.Name == number);
    }
}
