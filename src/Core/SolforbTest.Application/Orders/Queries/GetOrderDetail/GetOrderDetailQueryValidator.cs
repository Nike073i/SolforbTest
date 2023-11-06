using FluentValidation;

namespace SolforbTest.Application.Orders.Queries.GetOrderDetail
{
    public class GetOrderDetailQueryValidator : AbstractValidator<GetOrderDetailQuery>
    {
        public GetOrderDetailQueryValidator()
        {
            RuleFor(query => query.OrderId).GreaterThan(0);
        }
    }
}
