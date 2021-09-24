using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShop.API.Database;
using WebShop.API.Database.Entities;

namespace WebShop.API.Repository
{
    public interface IOrderItemRepository
    {
        Task<OrderItem> CreateOrderItem(OrderItem orderItem);
        Task<List<OrderItem>> CreateOrderItem(List<OrderItem> items);
        Task<OrderItem> UpdateOrderItem(int orderItemId, OrderItem orderItem);
        Task<OrderItem> DeleteOrderItem(int orderItemId);
    }
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly WebShopContext _context;
        public OrderItemRepository(WebShopContext context)
        {
            _context = context;
        }
        public async Task<OrderItem> CreateOrderItem(OrderItem orderItem)
        {
            _context.OrderItem.Add(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }
        public async Task<List<OrderItem>> CreateOrderItem(List<OrderItem> orderItems)
        {
            
                _context.OrderItem.AddRange(orderItems);
            
            await _context.SaveChangesAsync();
            return orderItems;
        }
        public async Task<OrderItem> DeleteOrderItem(int orderItemId)
        {
            OrderItem orderItem = await _context.OrderItem.FirstOrDefaultAsync(a => a.Id == orderItemId);
            if (orderItem != null)
            {
                _context.OrderItem.Remove(orderItem);
                await _context.SaveChangesAsync();
            }
            return orderItem;
        }
        public async Task<OrderItem> UpdateOrderItem(int orderItemId, OrderItem orderItem)
        {
            OrderItem updateOrderItem = await _context.OrderItem.FirstOrDefaultAsync(a => a.Id == orderItemId);
            if (updateOrderItem != null)
            {
                updateOrderItem.Amount = orderItem.Amount;
                updateOrderItem.CurrentPrice = orderItem.CurrentPrice;
                updateOrderItem.OrderId = orderItem.OrderId;
                await _context.SaveChangesAsync();

            }
            return updateOrderItem;
        }
    }
}
