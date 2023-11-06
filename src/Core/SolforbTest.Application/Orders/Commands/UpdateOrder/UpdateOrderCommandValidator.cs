using FluentValidation;

namespace SolforbTest.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(command => command.OrderId).GreaterThan(0);
            RuleFor(command => command)
                .Must(BeValidUpdateDto)
                .WithMessage("Хотя бы одно из полей для обновления должно иметь значение");

            RuleFor(command => command.Number).NotEmpty().When(command => command.Number != null);
            RuleFor(command => command.Date)
                .InclusiveBetween(new DateTime(2000, 1, 1), new DateTime(2100, 1, 1))
                .When(command => command.Date.HasValue);

            RuleFor(command => command.ProviderId)
                .GreaterThan(0)
                .When(command => command.ProviderId.HasValue);
        }

        private static bool BeValidUpdateDto(UpdateOrderCommand command) =>
            !string.IsNullOrEmpty(command.Number)
            || command.Date.HasValue
            || command.ProviderId.HasValue;
    }
}
