using System.ComponentModel.DataAnnotations;

namespace SolforbTest.WebClient.Models.BindingModels
{
    public class OrderItemBindingModel
    {
        [Required(ErrorMessage = "Идентификатор заказа должен быть установлен")]
        public int OrderId { get; }
        public int? OrderItemId { get; set; }

        [Required(ErrorMessage = "Наименование позиции должно быть указано")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Количество должно быть указано")]
        [Range(
            typeof(decimal),
            "1",
            "10000000",
            ErrorMessage = "Значение должно быть от {1} до {2}"
        )]
        public decimal? Quantity { get; set; }

        [Required(ErrorMessage = "Единица измерения должна быть указана")]
        public string? Unit { get; set; }

        public OrderItemBindingModel(int orderId)
        {
            OrderId = orderId;
        }
    }
}
