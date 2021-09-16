using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.API.Database;
using WebShop.API.Database.Entities;

namespace WebShop.API.Repository
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItem>> GetAllOrderItems();
        Task<OrderItem> GetOrderItemById(int orderItemId);
        Task<OrderItem> CreateOrderItem(OrderItem customer);
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

        public async Task<List<OrderItem>> GetAllOrderItems()
        {
            return await _context.OrderItem
                .ToListAsync();
        }

        public async Task<OrderItem> GetOrderItemById(int orderItemId)
        {
            return await _context.OrderItem
                .FirstOrDefaultAsync(a => a.Id == orderItemId);
        }

        public async Task<OrderItem> CreateOrderItem(OrderItem orderItem)
        {
            _context.OrderItem.Add(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
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
