using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.API.DTO.Requests;
using WebShop.API.DTO.Responses;

namespace WebShop.API.Services
{
    public interface IOrderService
    {
        Task<List<OrderResponse>> GetAllOrders();
        Task<OrderResponse> GetOrderById(int orderId);
        Task<OrderResponse> CreateOrder(NewOrder newOrder);
    }
    public class OrderService
    {
    }
}
