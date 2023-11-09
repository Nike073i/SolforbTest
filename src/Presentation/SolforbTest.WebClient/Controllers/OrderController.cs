using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolforbTest.Application.Orders.Commands.DeleteOrder;

namespace SolforbTest.WebClient.Controllers
{
    [Route("orders")]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{orderId:int:min(1)}")]
        public IActionResult Index(int orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }

        // Form в HTML5 имеет только post & get.
        [HttpPost("delete/{orderId:int:min(1)}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int orderId)
        {
            await _mediator.Send(new DeleteOrderCommand(orderId));
            return RedirectToAction("Index", "Home");
        }
    }
}
