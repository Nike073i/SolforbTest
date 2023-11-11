namespace SolforbTest.WebClient.Models.ViewModels
{
    public record OrderInfoViewModel(
        int OrderId,
        string OrderNumber,
        string ProviderName,
        DateTime Date
    );
}
