using Microsoft.AspNetCore.Mvc;

namespace SolforbTest.WebClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(
            int pageNumber = 1,
            int pageSize = 5,
            IEnumerable<string>? orderItemNames = null,
            IEnumerable<string>? orderItemUnits = null,
            IEnumerable<string>? orderNumbers = null,
            IEnumerable<int>? providerIds = null,
            DateTime? periodStart = null,
            DateTime? periodEnd = null
        )
        {
            ViewBag.PageNumber = pageNumber > 0 ? pageNumber : 1;
            ViewBag.PageSize = pageSize > 0 ? pageSize : 5;
            ViewBag.OrderItemNamesFilter = orderItemNames ?? Enumerable.Empty<string>();
            ViewBag.OrderItemUnitsFilter = orderItemUnits ?? Enumerable.Empty<string>();
            ViewBag.OrderNumbersFilter = orderNumbers ?? Enumerable.Empty<string>();
            ViewBag.ProviderIdsFilter = providerIds ?? Enumerable.Empty<int>();
            ViewBag.PeriodStart = periodStart;
            ViewBag.PeriodEnd = periodEnd;
            return View();
        }
    }
}
