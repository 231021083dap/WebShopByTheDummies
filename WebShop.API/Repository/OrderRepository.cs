using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShop.API.Database;
using WebShop.API.Database.Entities;

namespace WebShop.API.Repository
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int orderId);
        Task<Order> CreateOrder(Order order);
        Task<Order> UpdateOrder(int orderId, Order order);
        //Delete Order should not be an option
        //Task<Order> DeleteOrder(int orderId);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly WebShopContext _context;
        public OrderRepository(WebShopContext context)
        {
            _context = context;

        }

        #region Get All Orders
        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Order
                .Include(o => o.OrderItems)
                .Include(o => o.ShippingAddress)
                .Include(o => o.BillingAddress)
                .ToListAsync();
        }
        #endregion

        #region Get Order By Id
        public async Task<Order> GetOrderById(int orderId)
        {
            return await _context.Order
                .Include(o => o.ShippingAddress)
                .Include(o => o.BillingAddress)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Product)
                .FirstOrDefaultAsync(a => a.Id == orderId);
        }
        #endregion

        #region Create Order
        public async Task<Order> CreateOrder(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
        // Delete Order should not be an option!!!
        //public async Task<Order> DeleteOrder(int bookId)
        //{
        //    Order order = await _context.Order.FirstOrDefaultAsync(a => a.Id == orderId);
        //    if (order != null)
        //    {
        //        _context.Order.Remove(order);
        //        await _context.SaveChangesAsync();
        //    }
        //    return order;
        //}
        #endregion

        #region Update Order
        public async Task<Order> UpdateOrder(int orderId, Order order)
        {
            Order updateOrder = await _context.Order.FirstOrDefaultAsync(a => a.Id == orderId);
            if (updateOrder != null)
            {
                
                updateOrder.UpdatedDate = DateTime.Now;
                updateOrder.BillingAddress = order.BillingAddress;
                updateOrder.ShipmentAddressId = order.ShipmentAddressId;
                
                await _context.SaveChangesAsync();

            }
            return updateOrder;
        }
        #endregion
    }
}
