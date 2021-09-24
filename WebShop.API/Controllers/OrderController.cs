using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.API.DTO.Requests;
using WebShop.API.DTO.Responses;
using WebShop.API.Services;

namespace WebShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                List<OrderResponse> orders = await _orderService.GetAllOrders();
                if (orders == null)
                {
                    return Problem("No data exists on orders, not even an empty list, this is unexpected");
                }
                if (orders.Count == 0)
                {
                    return NoContent();

                }
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrderById([FromRoute] int orderId)
        {
            try
            {
                OrderResponse order = await _orderService.GetOrderById(orderId);
                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [HttpPut("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOrder([FromRoute] int orderId, [FromBody] UpdateOrder updateOrder)
        {
            try
            {
                OrderResponse orderResponse = await _orderService.UpdateOrder(orderId, updateOrder);
                if (orderResponse == null)
                {
                    return Problem("Order was not updated, something went wrong");
                }
                return Ok(orderResponse);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [HttpPut("OrderItem/{orderItemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOrderItem([FromRoute] int orderItemId, [FromBody] UpdateOrderItem updateOrderItem)
        {
            try
            {
                OrderItemResponse orderItemResponse = await _orderService.UpdateOrderItem(orderItemId, updateOrderItem);
                if (orderItemResponse == null)
                {
                    return Problem("OrderItem was not updated, something went wrong");
                };
                return Ok(orderItemResponse);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
    }
}
