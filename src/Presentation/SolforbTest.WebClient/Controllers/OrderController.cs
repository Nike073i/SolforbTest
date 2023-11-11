using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolforbTest.Application.Orders.Commands.CreateOrder;
using SolforbTest.Application.Orders.Commands.DeleteOrder;
using SolforbTest.Application.Orders.Commands.UpdateOrder;
using SolforbTest.WebClient.Models.Dto;

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

        [HttpGet("create")]
        public IActionResult Create() => View("Create");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(CreateOrderCommand createDtoCommand)
        {
            int orderId = await _mediator.Send(createDtoCommand);
            return RedirectToAction("Index", new { orderId });
        }

        // Form в HTML5 имеет только post & get.
        [HttpPost("{orderId:int:min(1)}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int orderId)
        {
            await _mediator.Send(new DeleteOrderCommand(orderId));
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("{orderId:int:min(1)}/update")]
        public IActionResult Update(int orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }

        // Form в HTML5 имеет только post & get.
        [HttpPost("{orderId:int:min(1)}/update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePost(UpdateOrderDto updateDto)
        {
            int orderId = await _mediator.Send(
                new UpdateOrderCommand(
                    updateDto.OrderId,
                    updateDto.Number,
                    updateDto.Date,
                    updateDto.ProviderId
                )
            );
            return RedirectToAction("Index", new { orderId });
        }
    }
}
