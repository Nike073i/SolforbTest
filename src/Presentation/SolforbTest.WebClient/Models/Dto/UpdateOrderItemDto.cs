namespace SolforbTest.WebClient.Models.Dto
{
    public record UpdateOrderItemDto(
        int OrderId,
        int OrderItemId,
        string Name,
        decimal Quantity,
        string Unit
    );
}
