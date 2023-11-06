using FluentValidation;
using SolforbTest.Application.Orders.Dto;

namespace SolforbTest.Application.Orders.Commands.UpdateOrderItem
{
    public class UpdateOrderItemDtoValidator : AbstractValidator<UpdateOrderItemDto>
    {
        public UpdateOrderItemDtoValidator()
        {
            RuleFor(command => command.OrderItemId).GreaterThan(0);

            RuleFor(command => command)
                .Must(BeValidUpdateDto)
                .WithMessage("Хотя бы одно из полей для обновления должно иметь значение");

            RuleFor(command => command.Name).NotEmpty().When(command => command.Name != null);

            RuleFor(command => command.Quantity)
                .GreaterThan(0)
                .When(command => command.Quantity.HasValue);

            RuleFor(command => command.Unit).NotEmpty().When(command => command.Name != null);
        }

        private static bool BeValidUpdateDto(UpdateOrderItemDto command) =>
            !string.IsNullOrEmpty(command.Name)
            || command.Quantity.HasValue
            || !string.IsNullOrEmpty(command.Unit);
    }
}
