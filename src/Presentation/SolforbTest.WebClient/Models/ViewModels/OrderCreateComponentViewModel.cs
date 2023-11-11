using Microsoft.AspNetCore.Mvc.Rendering;
using SolforbTest.Application.Providers.Queries.GetProviderList;
using SolforbTest.WebClient.Models.BindingModels;
using System.Globalization;

namespace SolforbTest.WebClient.Models.ViewModels
{
    public record OrderCreateComponentViewModel
    {
        public OrderInfoBindingModel OrderBindingModel { get; }

        public OrderCreateComponentViewModel(IEnumerable<ProviderViewModel> providers)
        {
            OrderBindingModel = new OrderInfoBindingModel(
                providers.Select(
                    p => new SelectListItem(p.Name, p.Id.ToString(CultureInfo.InvariantCulture))
                )
            );
        }
    }
}
