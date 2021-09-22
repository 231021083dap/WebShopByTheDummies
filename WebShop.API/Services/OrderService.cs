using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.API.Database.Entities;
using WebShop.API.DTO.Requests;
using WebShop.API.DTO.Responses;
using WebShop.API.Repository;

namespace WebShop.API.Services
{
    public interface IOrderService
    {
        Task<List<OrderResponse>> GetAllOrders();
        Task<OrderResponse> GetOrderById(int orderId);
        Task<OrderResponse> CreateOrder(NewOrder newOrder);
        
        Task<OrderResponse> UpdateOrder(int orderId, UpdateProduct updateProduct);
    }
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }
        public async Task<OrderResponse> CreateOrder(NewOrder newOrder)
        {
            Order order = new Order
            {
                OrderDate = DateTime.Now,
                ShipmentAddressId = newOrder.ShipmentAddressId,
                BillingAddressId = newOrder.BillingAddressId,

            };
            order = await _orderRepository.CreateOrder(order);
            return order == null ? null : new OrderResponse
            {

            };
        }

        public Task<List<OrderResponse>> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Task<OrderResponse> GetOrderById(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderResponse> UpdateOrder(int orderId, UpdateProduct updateProduct)
        {
            throw new NotImplementedException();
        }
    }
}
