using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SolforbTest.WebClient.Models.BindingModels
{
    public class OrderInfoBindingModel
    {
        public int? OrderId { get; set; }

        [Required(ErrorMessage = "Номер должен быть указан")]
        public string? Number { get; set; }

        [Required(ErrorMessage = "Дата должна быть указана")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Поставщик должен быть указан")]
        public int? SelectedProvider { get; set; }

        public IEnumerable<SelectListItem> Providers { get; }

        public OrderInfoBindingModel(IEnumerable<SelectListItem> providers)
        {
            Providers = providers;
        }
    }
}
