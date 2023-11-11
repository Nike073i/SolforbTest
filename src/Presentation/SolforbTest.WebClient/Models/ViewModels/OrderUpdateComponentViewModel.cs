using Microsoft.AspNetCore.Mvc.Rendering;
using SolforbTest.Application.OrderItems.Dto;
using SolforbTest.Application.Orders.Queries.GetOrderDetail;
using SolforbTest.Application.Providers.Queries.GetProviderList;
using SolforbTest.WebClient.Models.BindingModels;
using System.Globalization;

namespace SolforbTest.WebClient.Models.ViewModels
{
    public record OrderUpdateComponentViewModel
    {
        public OrderInfoBindingModel UpdateInfoModel { get; }
        public OrderItemsViewModel OrderItems { get; }

        public OrderUpdateComponentViewModel(
            OrderDetailViewModel orderDetails,
            IEnumerable<ProviderViewModel> providers,
            IEnumerable<OrderItemViewModel> orderItems
        )
        {
            UpdateInfoModel = new OrderInfoBindingModel(
                providers.Select(
                    p => new SelectListItem(p.Name, p.Id.ToString(CultureInfo.InvariantCulture))
                )
            )
            {
                OrderId = orderDetails.OrderId,
                Date = orderDetails.Date,
                Number = orderDetails.Number,
                SelectedProvider = orderDetails.ProviderId
            };
            OrderItems = new OrderItemsViewModel(orderDetails.OrderId, orderItems, true);
        }
    }
}
