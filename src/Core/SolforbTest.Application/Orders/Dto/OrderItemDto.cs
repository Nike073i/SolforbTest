namespace SolforbTest.Application.Orders.Dto
{
    public record CreateOrderItemDto(string Name, decimal Quantity, string Unit);

    public record UpdateOrderItemDto(
        int OrderItemId,
        string? Name = null,
        decimal? Quantity = null,
        string? Unit = null
    );
}
