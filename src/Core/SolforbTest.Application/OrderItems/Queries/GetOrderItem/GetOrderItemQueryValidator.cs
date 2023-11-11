using FluentValidation;

namespace SolforbTest.Application.OrderItems.Queries.GetOrderItem
{
    public class GetOrderItemQueryValidator : AbstractValidator<GetOrderItemQuery>
    {
        public GetOrderItemQueryValidator()
        {
            RuleFor(query => query.OrderItemId).GreaterThan(0);
        }
    }
}
