namespace SolforbTest.WebClient.Models.Dto
{
    public record AddOrderItemDto(
        int OrderId,
        string Name,
        decimal Quantity,
        string Unit
    );
}
