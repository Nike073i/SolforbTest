using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolforbTest.Application.OrderItems.Commands.AddOrderItem;
using SolforbTest.Application.OrderItems.Commands.RemoveOrderItem;
using SolforbTest.Application.OrderItems.Commands.UpdateOrderItem;
using SolforbTest.Application.OrderItems.Dto;
using SolforbTest.WebClient.Models.Dto;

namespace SolforbTest.WebClient.Controllers
{
    [Route("orders/{orderId:int:min(1)}")]
    public class OrderItemController : Controller
    {
        private readonly IMediator _mediator;

        public OrderItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Form в HTML5 имеет только post & get.
        [HttpPost("removeItem/{orderItemId:int:min(1)}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveItem(RemoveOrderItemCommand removeOrderItemCommand)
        {
            await _mediator.Send(removeOrderItemCommand);
            return RedirectToAction("Update", "Order", new { removeOrderItemCommand.OrderId });
        }

        [HttpGet("addItem")]
        public IActionResult AddItem(int orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }

        [HttpPost("addItem")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItemPost(AddOrderItemDto addDto)
        {
            await _mediator.Send(
                new AddOrderItemCommand(
                    addDto.OrderId,
                    new OrderItemDto(addDto.Name, addDto.Quantity, addDto.Unit)
                )
            );
            await _mediator.Send(addDto);
            return RedirectToAction("Update", "Order", new { addDto.OrderId });
        }

        [HttpGet("updateItem/{orderItemId:int:min(1)}")]
        public IActionResult UpdateItem(int orderId, int orderItemId)
        {
            ViewBag.OrderId = orderId;
            ViewBag.OrderItemId = orderItemId;
            return View();
        }

        // Form в HTML5 имеет только post & get.
        [HttpPost("updateItem")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateItemPost(UpdateOrderItemDto updateDto)
        {
            await _mediator.Send(
                new UpdateOrderItemCommand(
                    updateDto.OrderId,
                    updateDto.OrderItemId,
                    new OrderItemDto(updateDto.Name, updateDto.Quantity, updateDto.Unit)
                )
            );
            return RedirectToAction("Update", "Order", new { updateDto.OrderId });
        }
    }
}
