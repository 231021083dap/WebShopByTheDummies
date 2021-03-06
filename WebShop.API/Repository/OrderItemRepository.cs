using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShop.API.Database;
using WebShop.API.Database.Entities;

namespace WebShop.API.Repository
{
    public interface IOrderItemRepository
    {
        //Task<OrderItem> CreateOrderItem(OrderItem orderItem);
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

        #region Create Order Item
        public async Task<List<OrderItem>> CreateOrderItem(List<OrderItem> orderItems)
        {
            
                _context.OrderItem.AddRange(orderItems);
            
            await _context.SaveChangesAsync();
            return orderItems;
        }
        #endregion

        #region Delete OrderItem
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
        #endregion

        #region Update OrderItem
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
        #endregion
    }
}
